using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentationManager : MonoBehaviour
{

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject remainderPrefab;
    [SerializeField] private CalculatorController calculatorController;

    private List<GameObject> internalCubes;


    // Start is called before the first frame update
    void Start()
    {
        internalCubes = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void UpdateView()
    {
        // Destroying here to make way for new
        DestroyCubes();
        // Remembering position = -1 means that the list is empty
        if (calculatorController.position >= 0)
        {
            double value = calculatorController.inputs[calculatorController.position].value;
            int cube = SmallestCube(value);
            Debug.LogFormat("cube : {0}", cube);

            if (value > 0)
            {
                // Opportunity to make a cube

                double remainder = value - Mathf.Pow(cube, 3);
                int remainderWhole = (int)remainder;
                double remainderDecimals = remainder - remainderWhole;

                for (int i = 0; i < cube; i++)
                    for (int j = 0; j < cube; j++)
                        for (int k = 0; k < cube; k++)
                            MakeCube(new Vector3 { x = i, y = j, z = k }, false);

                Debug.LogFormat("RemainderWhole {0}", remainderWhole);

                if (remainderWhole > 0)
                    for (int i = 0; i < cube + 1; i++)
                    {
                        if (remainderWhole == 0) break;

                        for (int j = 0; j < cube + 1; j++)
                        {
                            if (remainderWhole == 0) break;

                            for (int k = 0; k < cube + 1; k++)
                            {
                                if (remainderWhole == 0) break;
                                MakeCube(new Vector3 { x = cube + i, y = j, z = k }, true);
                                remainderWhole--;
                            }
                        }
                    }

                // TODO for now ignoring decimals aka remainderDecimals


            }
            else
            {
                // TODO
                // What to do for negative numbers??
            }

        }

    }

    // Listeners for the controller
    void Awake()
    {
        Messenger.AddListener(GameEvent.UPDATE_HISTORY, UpdateView);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.UPDATE_HISTORY, UpdateView);

    }


    void DestroyCubes()
    {
        foreach (GameObject g in internalCubes)
        {
            Destroy(g);
        }
        internalCubes.Clear();
    }

    int SmallestCube(double number)
    {
        if (number > 0) return (int)System.Math.Pow(number, 1.0 / 3.0);
        else return (int)System.Math.Pow(number * -1, 1.0 / 3.0);
    }

    void MakeCube(Vector3 point, bool remainder)
    {
        if (remainder)
        {
            internalCubes.Add(Instantiate(remainderPrefab, point, Quaternion.identity));
        }
        else
        {
            internalCubes.Add(Instantiate(cubePrefab, point, Quaternion.identity));

        }
    }

}
