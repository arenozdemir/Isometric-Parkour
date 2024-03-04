using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundState : BaseState
{
    [SerializeField] Transform _camera;
    private float velocityY;
   
    private float gravityMultiplier = 100f;
    [SerializeField] BaseState jumpState;
    [SerializeField] BaseState dashState;

    private void OnEnable()
    {
        //player.isGrounded= true;
        //Yavuz bunu kapatt�m ��mk� oyunu k�t� anlamda etkiliyor. Mesela biz playerscriptin i�inde zaten karakter yere de�idi�inde isGroundu true yap�yoruz. 
        //Ama �rnek veriyorum bu a��k kald���nda diyelim ki z�plad�m ve havada dash att�m dashten hemen sonra OnGroundaState'e ge�iyor ve havadayken 
        //isGrounded true olmu� oluyor, ve ben havada defalarca tekrar tekrar z�playabiliyorum. ��nk� tekrar z�plamak i�in tek �art�m�z isGroundedin true olmas�.
        //(�u an sorunsuz �al���yor)
    }
    void Update()
    {
        //velocityY -= Time.deltaTime * gravityMultiplier;
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVector = _camera.right * inputVector.x + _camera.forward * inputVector.z;

        playerRb.velocity = moveVector * 5 + playerRb.velocity.y * Vector3.up;
        if(Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            GoToNextState(jumpState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
            GoToNextState(dashState);
        player.transform.forward = moveVector;
    }
}
