using System;
using UnityEngine;

[Serializable]
public class IRModel_HullWhite
{
    private double currentRate;
    private double meanReversion;
    private double volatility;
    private double longTermRate;

    public IRModel_HullWhite(double currentMarketRate, double meanReversion, double volatility, double longTermRate = 0.5)
    {
        currentRate = currentMarketRate;
        this.meanReversion = meanReversion;
        this.volatility = volatility;
        this.longTermRate = longTermRate;
        
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

    /// <summary>
    /// Volatility is the measure of how much the interest rate changes over time.
    /// A higher volatility means that the interest rate is more unstable.
    /// </summary>
    /// <param name="newVolatility"></param>
    public void SetVolatility(double newVolatility)
    {
        volatility = newVolatility;
    }

    /// <summary>
    /// This is the current interest rate of the bond.
    /// Be aware that changing this value will change the prediction of the future interest rates.
    /// </summary>
    /// <param name="newRate">The rate should be a positive number</param>
    public void SetCurrentRate(double newRate)
    {
        currentRate = newRate;
    }
    /// <summary>
    /// This skews the interest rate towards the mean.
    /// </summary>
    /// <param name="newMeanReversion"></param>
    public void SetMeanReversion(double newMeanReversion)
    {
        meanReversion = newMeanReversion;
    }
    public void SetLongTermRate(double newLongTermRate)
    {
        longTermRate = newLongTermRate;
    }
}
