using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    [SerializeField] private Transform[] LanePoints;
    private int LaneCount;
    [SerializeField] private PoolController CarPool;
    [SerializeField] private PlayerController PlayerMotor;

    List<CarController> ActiveCars = new List<CarController>(); 

    bool IsFirstCar = true;

    void Awake()
    {
        LaneCount = LanePoints.Length;
        Instance = this;
        InvokeRepeating("ControlCars", 0,0.5f);
        InvokeRepeating("CreateTraffic", 0,0.5f);
    }


    void Start()
    {
        //Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void CreateTraffic()
    {
        float carSpeed = Globals.Instance.GetCarSpeed();

        if(IsFirstCar)
        {
            IsFirstCar = false;

            Vector3 carPosition = Vector3.zero;
 

            for(int i=0; i<Globals.Instance.GetCarCount(); i++)
            {
                int leftLane = Random.Range(0,LaneCount/2);
                int rightLane = Random.Range(LaneCount/2,LaneCount);

                AddCar(carSpeed, leftLane,carPosition);
                carPosition.z += Globals.Instance.GetCarSpacing();
            }
        }
        else
        {
            int carCountLeft = 0;
            int carCountRight = 0;

            Vector3 carPositionLeft = Vector3.zero;
            Vector3 carPositionRight = Vector3.zero;

            foreach(CarController car in ActiveCars)
            {
                if(car.GetDirection() == -1) 
                {
                    carCountLeft++;
                    if(carPositionLeft.z < car.transform.position.z) carPositionLeft = car.transform.position;
                }
                else
                {
                    carCountRight++;
                    if(carPositionRight.z < car.transform.position.z) carPositionRight = car.transform.position;
                }
            }

            for(int i=0; i<Globals.Instance.GetCarCount() - carCountLeft; i++)
            {
                int newLane = Random.Range(LaneCount/2,LaneCount);
                AddCar(carSpeed, newLane,carPositionLeft);
                carPositionLeft.z += Globals.Instance.GetCarSpacing();
            }

            for(int i=0; i<Globals.Instance.GetCarCount() - carCountRight; i++)
            {
                int newLane = Random.Range(0,LaneCount/2);
                AddCar(carSpeed, newLane,carPositionRight);
                carPositionRight.z += Globals.Instance.GetCarSpacing();
            }


        }
    }

    void AddCar(float speed, int lane, Vector3 carPosition)
    {
        Vector3 newPosition = carPosition;
        newPosition.z += Globals.Instance.GetCarSpacing();
        newPosition.x = GetLanePosition(lane).x;

        GameObject newCar = CarPool.GetFromPool();
        CarController newController = newCar.GetComponent<CarController>();
        newController.InitCar(lane, GetLaneDirection(lane), speed, newPosition);
        ActiveCars.Add(newController);

    }

    void ControlCars()
    {
        if(ActiveCars.Count > 0)
        {
            List<CarController> disableList = new List<CarController>();
            bool anyDisable = false;

            foreach(CarController car in ActiveCars)
            {
                if(car.transform.position.z < PlayerMotor.transform.position.z - 50)
                {
                    disableList.Add(car);
                    anyDisable = true;
                }
            }

            if(anyDisable)
            {
                foreach(CarController car in disableList)
                {
                    ActiveCars.Remove(car);
                    car.DisableCar();
                    CarPool.AddToPool(car.gameObject);
                }
            }
        }    
    }

    public Vector3 GetLanePosition(int lane)
    {
        Vector3 pos = Vector3.zero;
        pos = LanePoints[lane].position;

        return pos;
    }

    public int GetLaneDirection(int lane)
    {
        int dir = -1;
        if(lane < LaneCount /2) dir = 1;

        return dir;
    }

    public Vector3 GetPlayerPosition()
    {
        return PlayerMotor.transform.position;
    }
}
