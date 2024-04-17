using System;
using UnityEngine;

[Serializable]
public class IRModel_HullWhite
{
    private double currentRate;
    private double meanReversion;
    private double volatility;

    public IRModel_HullWhite(double currentMarketRate, double meanReversion, double volatility)
    {
        currentRate = currentMarketRate;
        this.meanReversion = meanReversion;
        this.volatility = volatility;
    }

    public double[] predictIRforTimeInterval(float dt, float timeHorizon, double longTermRate = 0.5)
    {
        int steps = (int)(timeHorizon / dt); //
        double[] interestRates = new double[steps];

        for (int i = 0; i < steps; i++)
        {
            //Calculate Drift
            double drift = meanReversion * (longTermRate - currentRate) * dt;

            //Calculate Diffusion
            double diffusion = volatility * BrownianNoise(dt);

            // Update Interest Rates
            currentRate += drift + diffusion;
            interestRates[i] = currentRate;
        }

        return interestRates;
    }
    /// <summary>
    /// Generate a random number from a standard normal distribution
    /// </summary>
    /// <returns></returns>
    private double BrownianNoise(float dt)
    {        return Mathf.Sqrt(dt) * UnityEngine.Random.Range(-1f, 1f);
    }

    public void UpdateVolatility(double newVolatility)
    {
        volatility = newVolatility;
    }
}
