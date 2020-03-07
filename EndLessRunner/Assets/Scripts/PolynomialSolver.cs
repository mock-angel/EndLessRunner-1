using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolynomialSolver : MonoBehaviour
{
    static public float solve4(float a, float b, float c, float d, float e){
            //
        float P = 8*a*c - 3*b*b;
        float R = b*b*b + 8*d*a*a - 4*a*b*c;//;
        float del0 = c*c - 3*b*d + 12*a*e;// 
        float del1 = 2*c*c*c - 9*b*c*d + 27*b*b*e + 27*a*d*d - 72*a*c*e;// ;
        
        print("P = "+P+", R= "+R+", del0= "+del0+", del1= "+del1);
        
        float p = (8*a*c - 3*b*b)/(8*a*a);
        float q = (b*b*b-4*a*b*c + 8*a*a*d)/(8*a*a*a);// in numerator;
        
        //Value of Q is mostly complex, when all roots are real.
//        float Q = Mathf.Pow((del1 + Mathf.Sqrt(del1*del1 - 4*del0*del0*del0))/2f, 1f / 3f);

        float phi = Mathf.Acos(del1/(2*Mathf.Sqrt(del0*del0*del0)));
        
        float S = 0.5f*Mathf.Sqrt(-(2f/3f)*p + (2f/3f*a)*Mathf.Sqrt(del0)*Mathf.Cos(phi/3f));
        
        float firstTerm = -b/(4*a);
        
        print("p = "+p+", q= "+q+", phi= "+phi+", S= "+S);
        
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
