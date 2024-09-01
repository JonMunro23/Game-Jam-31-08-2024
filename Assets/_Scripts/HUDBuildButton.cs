using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDBuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

  public static event Action<BuildingType> OnBuildingButtonClick;
  [SerializeField] Sprite activeImage;
  [SerializeField] Sprite defaultImage;

  [SerializeField] Sprite activeBackgroundImage;
  [SerializeField] Sprite defaultBackgroundImage;

  [SerializeField] BuildingType buildingType;

  [SerializeField] Button button;

  Image backgroundImage;
  Image iconImage;

  bool isActive = false;

  void OnEnable()
  {
    button.GetComponentInChildren<TMP_Text>().text = buildingType.ToString();

    backgroundImage = button.GetComponent<Image>();

    iconImage = button.transform.GetChild(0).GetComponent<Image>();
    iconImage.sprite = defaultImage;

    button.onClick.AddListener(OnButtonClicked);
    HUDBuildButton.OnBuildingButtonClick += OnHUDBuildingButtonClicked;
  }

  void OnDisable()
  {
    button.onClick.RemoveListener(OnButtonClicked);
    HUDBuildButton.OnBuildingButtonClick -= OnHUDBuildingButtonClicked;
  }

  void OnButtonClicked()
  {
    if (isActive)
    {
      OnBuildingButtonClick?.Invoke(BuildingType.None);
      return;
    }

    OnBuildingButtonClick?.Invoke(buildingType);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    Color targetBackgroundColor = new Color(1f, 0.75f, 0.75f);
    backgroundImage.color = targetBackgroundColor;

    if (isActive) return;

    backgroundImage.sprite = activeBackgroundImage;
    iconImage.sprite = activeImage;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    backgroundImage.color = Color.white;


    if (isActive) return;


    backgroundImage.sprite = defaultBackgroundImage;
    iconImage.sprite = defaultImage;

  }

  void OnHUDBuildingButtonClicked(BuildingType type)
  {
    bool wasThisButtonClicked = type == buildingType;

    if (wasThisButtonClicked && !isActive)
    {
      isActive = true;
      UpdateImagery();
      return;
    }


    if (isActive)
    {
      isActive = false;
      UpdateImagery();
    }
  }

  void UpdateImagery()
  {

    if (isActive)
    {
      backgroundImage.sprite = activeBackgroundImage;
      iconImage.sprite = activeImage;
      return;
    }

    backgroundImage.sprite = defaultBackgroundImage;
    iconImage.sprite = defaultImage;


  }

}
