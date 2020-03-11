using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolynomialSolver : MonoBehaviour
{
    static public float solve4(float a, float b, float c, float d, float e){
            //
        float P = 8f*a*c - 3f*b*b;
        float R = b*b*b + 8f*d*a*a - 4f*a*b*c;//;
        float del0 = c*c - 3f*b*d + 12f*a*e;// 
        float del1 = 2f*c*c*c - 9f*b*c*d + 27f*b*b*e + 27f*a*d*d - 72f*a*c*e;// ;
        
        print("P = "+P+", R= "+R+", del0= "+del0+", del1= "+del1);
        
        float p = (8f*a*c - 3f*b*b)/(8f*a*a);
        float q = (b*b*b-4f*a*b*c + 8f*a*a*d)/(8f*a*a*a);// in numerator;
        
        //Value of Q is mostly complex, when all roots are real.
//        float Q = Mathf.Pow((del1 + Mathf.Sqrt(del1*del1 - 4*del0*del0*del0))/2f, 1f / 3f);

        float phi = Mathf.Acos(del1/(2f*Mathf.Sqrt(del0*del0*del0)));
        
        float S = 0.5f*Mathf.Sqrt(-(2f/3f)*p + (2f/3f*a)*Mathf.Sqrt(del0)*Mathf.Cos(phi/3f));
        
        float firstTerm = -b/(4*a);
        
        print("p = "+p+", q= "+q+", phi= "+phi+", S= "+S);
        
        float secondTerm = 0.5f*Mathf.Sqrt(-4f*S*S - 2f*p + q/S);
        float x1 = firstTerm - S + secondTerm;
        float x2 = firstTerm - S - secondTerm;
        
        secondTerm = 0.5f*Mathf.Sqrt(-4f*S*S - 2f*p - q/S);
        float x3 = firstTerm + S + secondTerm;
        float x4 = firstTerm + S - secondTerm;
        
        print("x1 = "+x1+", x2= "+x2+", x3= "+x3+", x4= "+x4);
        
        if(x1>0) return x1;
        if(x2>0) return x2;
        if(x3>0) return x3;
        if(x4>0) return x4;
        return 0;
    }
    
    static public float solve2(float a, float b, float c){
        float x1 = (-b + Mathf.Sqrt((b*b) - (4f*a*c))) / (2f*a);
        float x2 = (-b - Mathf.Sqrt((b*b) - (4f*a*c))) / (2f*a);
        
        float desirable_x = 0;
        
        //Now pick which of the two x is desirable.
        if(x1 < 0 || x2 < 0){
            //Calculate if either is negative.
            if(x1< 0&& x2<0){
                print("Error");
                return 0;
            }
            if(x2 >= 0){
                desirable_x = x2;
            }else 
                desirable_x = x1;
        }
        else{
            if(x1>x2){
                desirable_x = x1;
            }
            else desirable_x = x2;
        }
        
        return desirable_x;
    }
}
