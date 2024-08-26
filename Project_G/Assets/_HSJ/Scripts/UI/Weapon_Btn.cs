using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
public class Weapon_Btn : MonoBehaviour
{
    [SerializeField]
    GameInput gameInput;
    [SerializeField]
    GunshipBarrel gunshipBarrel;
    [SerializeField]
    Image BtnImg;

    #region Colors 
    [SerializeField]
    private Color White;
    [SerializeField]
    private Color Red;
    [SerializeField]
    private Color Yellow;
    #endregion

    void Awake()
    {
        BtnImg = GetComponent<Image>();
        
    }

    void Start()
    {
        gameInput.OnBulletChange_Left += ChangeWeaponColor;
        gameInput.OnBulletChange_Right += ChangeWeaponColor;
    }

    private void GetColorType(EColorType type)
    {
        switch(type)
        {
            case EColorType.Red:
                BtnImg.color = Red;
                break;
            case EColorType.Yellow:
                BtnImg.color = Yellow;
                break;
            default:
                BtnImg.color = White;
                break;
        }
    }
    public void ChangeWeaponColor(object sender, System.EventArgs e)
    {
        GetColorType(gunshipBarrel.BulletType);
    }

    void OnDestroy()
    {
        gameInput.OnBulletChange_Left -= ChangeWeaponColor;
        gameInput.OnBulletChange_Right -= ChangeWeaponColor;
    }
}
