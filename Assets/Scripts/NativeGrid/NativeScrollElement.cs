using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class NativeScrollElement : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private GameObject _container;
    [SerializeField] private RawImage _targetImage;
    [SerializeField] private Texture2D _errorImg;

    private string _url;
    private bool _inited = false;
    private bool _loaded = false;

    public void Build(string Url)
    {
        _url = Url;
        _inited = true;
        _btn.onClick.AddListener(OpenImage);
        ActivateCard();
    }

    private void OpenImage()
    {
        ImageHendler.instance.OnSetUrl?.Invoke(_url);
        SceneController.instance.OnOpenScene?.Invoke("Viewer");
    }

    private void OnBecameVisible()
    {
        _container.SetActive(true);
        if (_inited) ActivateCard();
    }

    private void OnBecameInvisible()
    {
        _container.SetActive(false);
        Destroy(_targetImage.texture);
        _loaded = false;
    }

    private async void ActivateCard()
    {
        if (_loaded) return;
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
