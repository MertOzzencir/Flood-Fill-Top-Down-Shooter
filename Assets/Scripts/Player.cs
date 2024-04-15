using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity
{
    public float speed = 8f;
    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    protected override void Start()
    {
       
        base.Start();
        
        viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
    }
    private void Update()
    {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 velocity = input * speed;
        controller.Move(velocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(plane.Raycast(ray, out rayDistance))
        {
            Vector3 lookInput = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin, lookInput);
            controller.LookToMouseInput(lookInput);

        }
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();

        }



    }

}
