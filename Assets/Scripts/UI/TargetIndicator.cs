using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Image TargetIndicatorImage;

    public Image OffScreenTargetIndicator;

    public float outOfSightOffset = 20f;

    private float outOfSightOffest { get { return outOfSightOffset; } }


    private GameObject target;

    private Camera mainCamera;

    private RectTransform canvasRect;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
   
    public void InitialiseTargetIndicator(GameObject target, Camera mainCamera, Canvas canvas)
    {
        this.target = target;
        this.mainCamera = mainCamera;
        canvasRect = canvas.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    public void UpdateTargetIndicator()
    {
        SetIndicatorPosition();
    }
           
    

    protected void SetIndicatorPosition()
    {
        //Obtener la posición del objetivo en relación con el espacio de pantalla
        Vector3 indicatorPosition = mainCamera.WorldToScreenPoint(target.transform.position);
         
        //En caso de que el objetivo esté frente a la camara y dentro de los límites de su fracción
        if (indicatorPosition.z >= 0f & indicatorPosition.x <= canvasRect.rect.width * canvasRect.localScale.x
            & indicatorPosition.y <= canvasRect.rect.height * canvasRect.localScale.x & indicatorPosition.x >= 0f & indicatorPosition.y >= 0f)
        {
            //Seteo z en cero ya que no es necesario y solo causa problemas (demasiado lejos de la camara para mostrarse)
            indicatorPosition.z = 0f;

            //Target esta a la vista, cambie las piezas del indicador en consecuencia

            targetOutOfSight(false, indicatorPosition);
        }

        //En caso de que el target esta frente a nosotros, pero fuera de vision
        else if (indicatorPosition.z >= 0f)
        {
            //Seteo indicatorPosition y seteo targetIndicator a outOfSight
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);
        }
        else
        {
            //Invertir posicion del indicador. De lo contrario, la posicion del indicador se invertira si el objetivo esta en la parte posterior de la camara
            indicatorPosition *= -1f;

            //Seteo indicatorPosition y seteo targetIndicator a outOfSight
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);
        }

        //Seteo la posicion de el indicator
        rectTransform.position = indicatorPosition;
    }

    private Vector3 OutOfRangeindicatorPositionB(Vector3 indicatorPosition)
    {
        //Seteo z en cero ya que no es necesario y solo causa problemas (demasiado lejos de la camara para mostrarse)
        indicatorPosition.z = 0f;

        //Calculo el centro del canvas y lo resto desde la posicion del indicator para tener la coordenada desde el centro del canvas
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        //Calculo si el Vector al target intersecta con el borde y del canvas o con el x
        float divX = (canvasRect.rect.width / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.x);
        float divY = (canvasRect.rect.height / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.y);

        //En caso de que intersecte primero con el borde x primero, ponemos el x al borde y ajustamos el y con trigonometria
        if(divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (canvasRect.rect.width * 0.5f - outOfSightOffest) * canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
        }

        //En caso de que intersecte primero con el borde y, ponemos el y al borde y ajustamos el x
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);

            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (canvasRect.rect.height / 2f - outOfSightOffest) * canvasRect.localScale.y;
            indicatorPosition.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
        }

        //Cambio la posicion del indicator devuelta a las coordenadas del rectTransform y devuelvo la posicion del indicator
        indicatorPosition += canvasCenter;
        return indicatorPosition;
    }


    private void targetOutOfSight(bool oos, Vector3 indicatorPosition)
    {

        //En caso de que el indicator esta fuera de rango
        if (oos) //OutOufSight
        {
            //Activo y desactivo
            if (OffScreenTargetIndicator.gameObject.activeSelf == false) OffScreenTargetIndicator.gameObject.SetActive(true);
            if (TargetIndicatorImage.isActiveAndEnabled == true) TargetIndicatorImage.enabled = false;

            //Seteo la rotacion del OutOfSight 
            OffScreenTargetIndicator.rectTransform.rotation = Quaternion.Euler(rotationOutOfSightTargetindicator(indicatorPosition));
        }

        //En caso de que el indicator esta en rango, prendo el inSight y apago el OOS
        else
        {
            if (OffScreenTargetIndicator.gameObject.activeSelf == true) OffScreenTargetIndicator.gameObject.SetActive(false);
            if (TargetIndicatorImage.isActiveAndEnabled == false) TargetIndicatorImage.enabled = true;
        }

        
    }

    private Vector3 rotationOutOfSightTargetindicator(Vector3 indicatorPosition)
    {
        //Calculo el centro del canvas
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;


        //Calculo el angulo entre la posicion del indicator y la direccion hacia arriba
        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);

        //Retorno el angulo como un vector de rotacion
        return new Vector3(0f, 0f, angle);
    }
}
