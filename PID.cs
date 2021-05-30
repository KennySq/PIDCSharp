using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PID64
{
    public double mKp = 0.0;
    public double mKi = 0.0;
    public double mKd = 0.0;

    public double mTarget = 0.0;

    public double mError = 0.0;
    public double mPreviousError = 0.0;

    double mIntegral = 0.0;
    double mDerivative = 0.0;

    public double mMax;
    public double mMin;

    public double GetError() { return mError; }

    public double Calibrate(double delta, double current)
    {
        mError = mTarget - current;

        double proportional = mError * mKp;

        mIntegral += mError * delta;
        double integral = mIntegral * mKi;

        mDerivative = (mError - mPreviousError) / delta;
        double derivative = mKd * mDerivative;

        double result = proportional + integral + derivative;

        if(result > mMax)
        {
            result = mMax;
        }
        else if(result < mMin)
        {
            result = mMin;
        }

        mPreviousError = mError;

        return result;

    }

    public PID64(double target, double kp, double ki, double kd, double max, double min)
    {
        mKp = kp;
        mKi = ki;
        mKd = kd;

        mTarget = target;

        mMax = max;
        mMin = min;
    }


}

public class PID32
{
    public float mKp;
    public float mKi;
    public float mKd;

    public float mTarget;

    public float mError;
    public float mPreviousError;

    float mIntegral = 0.0f;
    float mDerivative;

    public float mMax;
    public float mMin;

    public float GetError() { return mError; }

    public float Calibrate(float delta, float current)
    {
        mError = mTarget - current;

        float proportional = mError * mKp;

        mIntegral += mError * delta;
        float integral = mIntegral * mKi;

        mDerivative = (mError - mPreviousError) / delta;
        float derivative = mKd * mDerivative;

        float result = proportional + integral + derivative;

        if (result > mMax)
        {
            result = mMax;
        }
        else if (result < mMin)
        {
            result = mMin;
        }

        mPreviousError = mError;

        return result;

    }

    public PID32(float target, float kp, float ki, float kd, float max, float min)
    {
        mKp = kp;
        mKi = ki;
        mKd = kd;

        mTarget = target;

        mMax = max;
        mMin = min;
    }


}
