using UnityEngine;

// Not used for now, but it will be used to generate the Wiener process noise (brownian).
public class Wiener : MonoBehaviour
{
    /*
    public double GenerateNoise
         (double initPos, double drift, double diffusion, double deltaTime, int numSteps)
    {
        double[] wienerProcess = new double[numSteps];
        double currentPos = initPos;

        for (int i = 0; i < numSteps; i++)
        {
            double randomValue = Random.Range(0f, 1.0f);
            double increment = drift * deltaTime + System.Math.Sqrt((float)deltaTime) * diffusion * randomValue;
            currentPos += increment;
            wienerProcess[i] = currentPos;
        }

        return wienerProcess;
     }*/
}
