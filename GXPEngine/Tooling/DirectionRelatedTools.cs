using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

namespace Tools
{
     class DirectionRelatedTools{

        //public static float CalculateAngle(float x1, float y1, float x2, float y2)
        //{

        //    float x = x2 - x1;
        //    float y = y2 - y1;
        //    double direction = Math.Atan(Math.Abs(y / x)) * (180.0d / Math.PI);

        //    if (x > 0 && y > 0)
        //        return (float)(direction);
        //    if (x < 0 && y > 0)
        //        return (float)(180.0f - direction);
        //    if (x < 0 && y < 0)
        //        return (float)(180.0f + direction);
        //    if (x > 0 && y < 0)
        //        return (float)(360.0f - direction);
        //    return -1.0f;

        //}

        public static float CalculateAngle(float x1, float y1, float x2, float y2)
        {

            float dx = x2 - x1;
            float dy = y2 - y1;
            double direction = Math.Atan2(dy,dx) * (180.0d / Math.PI);


            //if (x > 0 && y > 0)
            //    return (float)(direction);
            //if (x < 0 && y > 0)
            //    return (float)(180.0f - direction);
            //if (x < 0 && y < 0)
            //    return (float)(180.0f + direction);
            //if (x > 0 && y < 0)
            //    return (float)(360.0f - direction);
            //return -1.0f;
            return (float) (( direction + 360 ) % 360 );

        }

        public static float CalculateDistance(float x1,float y1,float x2,float y2) {
            return (float)(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
        }

    }
}

