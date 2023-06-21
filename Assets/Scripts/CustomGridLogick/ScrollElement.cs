using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScrollElement : MonoBehaviour
{

    [SerializeField] private Button _btn;
    [SerializeField] private RawImage _targetImage;
    [SerializeField] private Texture2D _errorImg;

    private string _url;

    private void OnEnable()
    {
        _btn.onClick.AddListener(OpenImage);
    }

    public void Build(string Url)
    {
        _url = Url;
        ActivateCard();
    }

    private void OpenImage()
    {
        ImageHendler.instance.OnSetUrl?.Invoke(_url);
        SceneController.instance.OnOpenScene?.Invoke("Viewer");
    }

    private async void ActivateCard()
    {
        StopAllCoroutines();
        if (_targetImage.texture != null) _targetImage.texture = null;

        var f = Resources.UnloadUnusedAssets();

        while (!f.isDone)
            await Task.Yield();

        _targetImage.texture = await Downloader.DownloadImage(_url);

        if (_targetImage.texture == null)
            _targetImage.texture = _errorImg;
    }
}
