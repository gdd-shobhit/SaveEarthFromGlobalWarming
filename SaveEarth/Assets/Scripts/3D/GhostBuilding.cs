using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding : Building
{

    public Transform visual;
    public Dir currentDir;
    public BuildingType currentType;
    public MeshRenderer rendererForBuilding;
    public enum Dir
    {
        Up,
        Right,
        Down,
        Left,
    }

    public enum BuildingType
    {
        Towncenter,
        House,
        Factory,
        FilterationPlant,
        Farm
    }

    // Start is called before the first frame update
    void Start()
    {
       currentDir = Dir.Up;
        currentType = BuildingType.House;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = MyGridSystem.instance.GetExactCenter(MyGridSystem.instance.GetMouseWorldPosition());
        targetPosition.y = 1f;

        if(buildingData != null && buildingData.size == 2)
            targetPosition += CalculateBigBuildingOffset();

        //targetPosition += CalculateOffset(currentDir);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 30f);
        // rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, GetEuler(currentDir), Time.deltaTime * 15f) ;

        
    }

    private Vector3 CalculateOffset(Dir incomingDir)
    {
        switch (incomingDir)
        {
            case Dir.Up: return new Vector3(0, 0, 1);
            case Dir.Right: return new Vector3(1, 0, 0);
            case Dir.Down: return new Vector3(0, 0, -1);
            case Dir.Left: return new Vector3(-1, 0, 0);
            default: return new Vector3(0,0,0);
        }
    }

    private Vector3 CalculateBigBuildingOffset()
    {
        return new Vector3(0.5f,0, 0.5f);
    }

    Quaternion GetEuler(Dir incomingDir)
    {
        switch(incomingDir)
        {
            case Dir.Up:
                {
                    return Quaternion.Euler(0, 0, 0);
                }
                
            case Dir.Right:
                {
                    return Quaternion.Euler(0, 90, 0);
                }

              
            case Dir.Down: return Quaternion.Euler(0, 180, 0);
            case Dir.Left: return Quaternion.Euler(0, 270, 0);
                default: return Quaternion.Euler(0, 0, 0);
        }
    }
}
