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
        //Yavuz bunu kapattým çümkü oyunu kötü anlamda etkiliyor. Mesela biz playerscriptin içinde zaten karakter yere deðidiðinde isGroundu true yapýyoruz. 
        //Ama örnek veriyorum bu açýk kaldýðýnda diyelim ki zýpladým ve havada dash attým dashten hemen sonra OnGroundaState'e geçiyor ve havadayken 
        //isGrounded true olmuþ oluyor, ve ben havada defalarca tekrar tekrar zýplayabiliyorum. Çünkü tekrar zýplamak için tek þartýmýz isGroundedin true olmasý.
        //(Þu an sorunsuz çalýþýyor)
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
