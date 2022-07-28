using System; // Serializable, Array
using System.Linq; // Max()
using Unity.Barracuda; // Model, NNModel, IWorker
using UnityEngine; // MonoBehaviour 
using TMPro; // TextMeshProUGUI
using UnityEditor; // SetTextureImporterFormat()
using UnityEngine.UI;

// 모델의 추론 과정을 모방한 클래스 
// shirt 0, tree 1
public class GetInferenceFromModel : MonoBehaviour
{
    public static GetInferenceFromModel instance;

    public static Texture2D texture; // 모델이 예측할 이미지 텍스처
    
    [Header(" ---Model---")]
    public NNModel modelAsset; // 학습된 모델
    private Model _runtimeModel; // 실행할 모델
    private IWorker _engine; // 모델을 돌릴 엔진

    // 예측값 구조체를 통해 필요한 기능 받아오기 
    public Prediction prediction;

    [Header("---Debugger---")]
    public TextMeshProUGUI result; // 결과값을 확인할 UI Text
    public RawImage testimage;
    public RawImage testimageafter;

    


    /// <summary>
    /// 인스펙터에서 쉽게 볼 수 있는 방식으로 예측 결과를 유지하는 데 사용되는 구조체.
    /// </summary>
    [Serializable]
    public struct Prediction
    {
        // 모델의 예측가능성 중 가장 높은 값
        public int predictedValue;
        // 라벨에 대한 예측값 배열
        public float[] predicted;

        // 텐서를 매개변수로 받아 예측값을 가져오는 메서드
        public void SetPrediction(Tensor tens)
        {
            // 부동 소수점 값 출력을 예측 배열로 추출
            predicted = tens.AsFloats();
            // 가장 가능성이 높은 것(=예측 값)의 인덱스 가져오기 
            predictedValue = Array.IndexOf(predicted, predicted.Max());
            Debug.Log($"Predicted {predictedValue}");
        }

    }
    private void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        // 런타임 모델 및 작업자를 설정
        _runtimeModel = ModelLoader.Load(modelAsset);
        // 실행 엔진 설정(worker 생성 (실행 모델, 실행할 엔진 : CPU, GPU, Auto))
        _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.CPU);
        // 예측 구조체 인스턴스화.
        prediction = new Prediction();
        result.text = "카메라가 켜졌습니다.";
    }
    
    // ModelExecute 버튼 누르면 실행될 함수
    public void PreModel()
    {
        TextValue();

        testimage.texture = texture; // 이미지 잘 불러와 지는지 확인
        texture = ScaleTexture(texture, 0.064f); // 이미지 전처리 : 리사이즈
  
        testimageafter.texture = texture; // 이미지 잘 불러와 지는지 확인

        var channelCount = 3; //1 = 회색조, 3 = 색상, 4 = 색상 알파
        // 텍스처에서 입력을 위한 텐서 생성.
        Tensor inputX = new Tensor(texture, channelCount); //(0, 64, 64,3)
        // 실행해서(Execute) 결과값 내보내기(PeekOutput)
        Tensor outputY = _engine.Execute(inputX).PeekOutput();
        // 출력 텐서를 사용하여 예측 구조체의 값을 설정
        prediction.SetPrediction(outputY);   
        // 예측값중 가장 높은 값 문자열로 변환해서 UI Text에 보여주기
        TextValue();
        // 입력 텐서를 수동으로 폐기(가비지 컬렉터 아님).
        inputX.Dispose();
    }

    // 비율로 해서 Resize하기
    public  Texture2D ScaleTexture( Texture2D source, float _scaleFactor)
    {
        if (_scaleFactor == 1f)
        {
            return source;
        }
        else if (_scaleFactor == 0f)
        {
            return Texture2D.blackTexture;
        }

        int _newWidth = Mathf.RoundToInt(source.width * _scaleFactor);
        int _newHeight = Mathf.RoundToInt(source.height * _scaleFactor);


        
        Color[] _scaledTexPixels = new Color[_newWidth * _newHeight];

        for (int _yCord = 0; _yCord < _newHeight; _yCord++)
        {
            float _vCord = _yCord / (_newHeight * 1f);
            int _scanLineIndex = _yCord * _newWidth;

            for (int _xCord = 0; _xCord < _newWidth; _xCord++)
            {
                float _uCord = _xCord / (_newWidth * 1f);

                _scaledTexPixels[_scanLineIndex + _xCord] = source.GetPixelBilinear(_uCord, _vCord);
            }
        }

        // 스케일 텍스처 생성
        Texture2D result = new Texture2D(_newWidth, _newHeight, source.format, false);
        result.SetPixels(_scaledTexPixels, 0);
        result.Apply();

        return result;
    }
    private void OnDestroy()
    {
        // 엔진을 수동으로 폐기합니다(가비지 컬렉터 아님).
        _engine?.Dispose();
    }

    private void TextValue()
    {
        result.text = "지미에게 보낼 선물을 고르고 있어요.";
        
        if(prediction.predictedValue == 0)
        {
             result.text = "셔츠 인식 성공! 선물을 보냈습니다.";
        }
        else if(prediction.predictedValue ==1 )
        {
            result.text = "나무 인식 성공! 선물을 보냈습니다.";
        }
    }
}


