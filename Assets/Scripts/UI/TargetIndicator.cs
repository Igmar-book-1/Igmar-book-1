using LineOfSight;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Image TargetIndicatorImageWhite;
    public Image TargetIndicatorImageRed;

    public Image OffScreenTargetIndicatorWhite;
    public Image OffScreenTargetIndicatorRed;

    public float outOfSightOffset = 20f;

    private float outOfSightOffest { get { return outOfSightOffset; } }


    private GameObject target;

    private Camera mainCamera;

    private RectTransform canvasRect;

    private RectTransform rectTransform;

    private float currentTimeVision;
    private float maxTimeVision;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(target!=null)
        {
            currentTimeVision = target.GetComponentInParent<LineOfSightBehaviour>().countDown;
            updateImages();

        }

    }
    public void updateImages()
    {
        float fillAmount = currentTimeVision / maxTimeVision;
        TargetIndicatorImageRed.fillAmount = fillAmount;
        TargetIndicatorImageWhite.fillAmount = fillAmount;
        OffScreenTargetIndicatorWhite.fillAmount = fillAmount;
        OffScreenTargetIndicatorRed.fillAmount = fillAmount;
    }

    public void InitialiseTargetIndicator(GameObject target, Camera mainCamera, Canvas canvas)
    {
        this.target = target;
        this.mainCamera = mainCamera;
        canvasRect = canvas.GetComponent<RectTransform>();
        maxTimeVision = target.GetComponentInParent<LineOfSightBehaviour>().maxCountDown;
    }
    // Update is called once per frame
    public void UpdateTargetIndicator()
    {
        SetIndicatorPosition();
    }

    public void turnOfTargetIndicator(Transform playerTarget)
    {

        if (Vector3.Distance(playerTarget.position, target.transform.position) > 20 || !target.GetComponentInParent<LineOfSightBehaviour>().IsInSight(playerTarget))
        {
            OffScreenTargetIndicatorWhite.gameObject.SetActive(false);
            OffScreenTargetIndicatorRed.gameObject.SetActive(false);
            TargetIndicatorImageWhite.enabled = false;
            TargetIndicatorImageRed.enabled = false;
        }
    }
           
    

    protected void SetIndicatorPosition()
    {
        currentTimeVision = target.GetComponentInParent<LineOfSightBehaviour>().countDown;
        updateImages();

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
        currentTimeVision = target.GetComponentInParent<LineOfSightBehaviour>().countDown;
        updateImages();
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
            if (OffScreenTargetIndicatorWhite.gameObject.activeSelf == false)
            {
                OffScreenTargetIndicatorWhite.gameObject.SetActive(true);
                OffScreenTargetIndicatorRed.gameObject.SetActive(true);
            }
            if (TargetIndicatorImageWhite.isActiveAndEnabled == true)
            {
                TargetIndicatorImageWhite.enabled = false;
                TargetIndicatorImageRed.enabled = false;
            }

            //Seteo la rotacion del OutOfSight 
            OffScreenTargetIndicatorWhite.rectTransform.rotation = Quaternion.Euler(rotationOutOfSightTargetindicator(indicatorPosition));
            OffScreenTargetIndicatorRed.rectTransform.rotation = Quaternion.Euler(rotationOutOfSightTargetindicator(indicatorPosition));
        }

        //En caso de que el indicator esta en rango, prendo el inSight y apago el OOS
        else
        {
            if (OffScreenTargetIndicatorWhite.gameObject.activeSelf == true)
            {
                OffScreenTargetIndicatorRed.gameObject.SetActive(false);
                OffScreenTargetIndicatorWhite.gameObject.SetActive(false);
            }
            if (TargetIndicatorImageWhite.isActiveAndEnabled == false)
            {
                TargetIndicatorImageWhite.enabled = true;
                TargetIndicatorImageRed.enabled = true;
            }
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

    public void turnOffCamera()
    {
        OffScreenTargetIndicatorWhite.gameObject.SetActive(false);
        TargetIndicatorImageWhite.enabled = false;
    }
}
