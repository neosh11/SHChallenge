using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentationManager : MonoBehaviour
{

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Material remainderMaterial;
    [SerializeField] private Material negativeMaterial;

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

            bool positive = value > 0;
            // Make positive number for processing purposes
            // We still know that it's negative
            if (!positive)
            {
                value *= -1;
            }
            // Opportunity to make a cube
            double remainder = value - Mathf.Pow(cube, 3);
            int remainderWhole = (int)remainder;
            double remainderDecimals = remainder - remainderWhole;

            for (int i = 0; i < cube; i++)
                for (int j = 0; j < cube; j++)
                    for (int k = 0; k < cube; k++)
                        MakeCube(new Vector3 { x = i, y = j, z = k }, false, positive);

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
                            MakeCube(new Vector3 { x = cube + i, y = j, z = k }, true, positive);
                            remainderWhole--;
                        }
                    }
                }

            // TODO for now ignoring decimals aka remainderDecimals

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

    void MakeCube(Vector3 point, bool remainder, bool positive)
    {
        if (remainder)
        {
            var x = Instantiate(cubePrefab, point, Quaternion.identity);
            x.GetComponent<Renderer>().material = remainderMaterial;
            internalCubes.Add(x);
        }
        else if (positive)
        {
            internalCubes.Add(Instantiate(cubePrefab, point, Quaternion.identity));

        }
        else
        {
            var x = Instantiate(cubePrefab, point, Quaternion.identity);
            x.GetComponent<Renderer>().material = negativeMaterial;
            internalCubes.Add(x);
        }
    }

}
