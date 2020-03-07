using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolynomialSolver : MonoBehaviour
{
    static public float solve4(float a, float b, float c, float e){
            //
        float P = 8*a*c - 3*b*b;
        float R = b*b*b -4*a*b*c;//+8*d*a*a;
        float del0 = c*c + 12*a*e;// - 3*b*d
        float del1 = 2*c*c*c + 27*b*b*e - 72*a*c*e;// - 9*b*c*d + 27*a*d*d;
        
        float p = (8*a*c - 3*b*b)/(8*a*a);
        float q = (b*b*b-4*a*b*c)/(8*a*a*a);//+8*a*a*d in numerator;
        
        float Q = Mathf.Pow((del1 + Mathf.Sqrt(del1*del1 - 4*del0*del0*del0))/2f, 1f / 3f);
        float S = 0.5f*Mathf.Sqrt(-(2f/3f)*p + (1f/3*a)*(Q + del0/Q));
        
        float firstTerm = -b/(4*a);
        
        float secondTerm = 0.5f*Mathf.Sqrt(-4*S*S - 2*p + q/S);
        float x1 = firstTerm - S + secondTerm;
        float x2 = firstTerm - S - secondTerm;
        
        secondTerm = 0.5f*Mathf.Sqrt(-4*S*S - 2*p - q/S);
        float x3 = firstTerm + S + secondTerm;
        float x4 = firstTerm + S - secondTerm;
        
        print("x1 = "+x1+", x2= "+x2+", x3= "+x3+", x4= "+x4);
        
        if(x1>0) return x1;
        if(x2>0) return x2;
        if(x3>0) return x3;
        if(x4>0) return x4;
        return 0;
    }
}
