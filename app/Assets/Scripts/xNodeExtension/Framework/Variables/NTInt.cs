using System;
using UnityEngine;

namespace NT.Variables
{

    [Serializable]
    public class NTInt : NTVariable<int>{

         public override bool Evaluate(Operator op, string value, bool isLeft){

            int intValue = 0;

            if(!int.TryParse(value, out intValue)){
                intValue = 0;
            }

            return InternalEvaluate(op, intValue, isLeft);
            
        }

        public override bool Evaluate(Operator op, NTVariable value){
            Type rightVariableType = value.GetDataType();

            if(rightVariableType == typeof(float) || rightVariableType == typeof(int) || rightVariableType == typeof(double)){
                int rightValue = (int) value.GetValue(); 
                return InternalEvaluate(op, rightValue, true);
            }
            else{
                return this.value > 0;
            }
            
        }

        private bool InternalEvaluate(Operator op, int value, bool isLeft){
            switch(op){
                case Operator.Equals:
                    return (this.value == value);
                case Operator.NotEquals:
                    return (this.value != value);
                case Operator.GreaterThan:
                    if(isLeft) return  this.value > value;
                    else return  value > this.value;                    
                case Operator.LessThan:
                    if(isLeft) return  this.value < value;
                    else return  value < this.value; 
                case Operator.GreaterOrEqualThan:
                    if(isLeft) return  this.value >= value;
                    else return  value >= this.value; 
                case Operator.LessOrEqualThan:
                    if(isLeft) return  this.value <= value;
                    else return  value <= this.value; 
                default:
                    return false;
            }
        }
    
    }
}