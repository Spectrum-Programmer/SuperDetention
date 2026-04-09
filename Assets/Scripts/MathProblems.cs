using System;
using System.Collections.Generic;
using static GlobalScript;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public abstract class MathProblems
{
    [Serializable]
    public struct MathExpression
    {
        public Operand operand1;
        public operation op;
        public Operand operand2;

        public MathExpression(Operand operand1, operation op, Operand operand2)
        {
            this.operand1 = operand1;
            this.op = op;
            this.operand2 = operand2;
        }
}

    [Serializable]
    public struct Operand
    {
        public int operand;
        public operation op;

        public Operand(int operand, operation op)
        {
            this.operand = operand;
            this.op = op;
        }
    }

    
    
    private static MathExpression[] GetEasyExpressions()
    {
        float[] probability_list = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        var expression_list = new MathExpression[5];

        for (var i = 0; i < 5; i++)
        {

            var found = false;
            
            for (var j = 0; j < 5; j++)
            {
                if (!(Random.value < probability_list[j])) continue;
                found = true;
                probability_list[j] -= 0.25f;
                var values = GenerateExpressionNums((operation)j);
                var value1 = new Operand(values[0], (operation)11);
                var value2 = new Operand(values[1], (operation)11);
                expression_list[i] = new MathExpression(value1, (operation)j, value2);
                break;
            }

            if (found) continue;
            var op = (operation)Random.Range(0, 5);
            var new_values = GenerateExpressionNums(op);
            var newValue1 = new Operand(new_values[0], (operation)11);
            var newValue2 = new Operand(new_values[1], (operation)11);
            expression_list[i] = new MathExpression(newValue1, op, newValue2);
        }

        return expression_list;
    }

    private static int[] GenerateExpressionNums(operation op)
    {
        switch (op)
        {
            case operation.Addition or operation.Subtraction or operation.Multiplication:
                return new[] {
                    Random.Range(0,100), 
                    Random.Range(0,100)
                };
            case operation.Division:
                var value1 = Random.Range(0, 10);
                var value2 = Random.Range(0, 10);
                var product = value1 * value2;
                return new [] { product, value1 };
            case operation.Modulus:
                return new[]
                {
                    Random.Range(100,300),
                    Random.Range(0,100)
                };
        }

        return null;
    }
    

    private static MathExpression[] GetHardExpressions()
    {
        var expression_list = new MathExpression[5];
        
        var simple_expression = GetEasyExpressions();
        
        var i = 0;
        
        foreach (var expr in simple_expression)
        {
            
            var new_expr = expr;
            
            var oper = expr.op;

            var advancedVal = GenerateAdvancedOperands();

            var advancedNum = SolveOperand(advancedVal);

            int operandNum;
            
            switch (oper)
            {
                case operation.Addition or operation.Subtraction:
                    operandNum = Random.Range(0, 2);
                    if (operandNum == 0) new_expr.operand1 = advancedVal;
                    else new_expr.operand2 = advancedVal;
                    break;
                case operation.Multiplication:
                    while (advancedNum > 100)
                    {
                        advancedVal = GenerateAdvancedOperands();
                        advancedNum = SolveOperand(advancedVal);
                    }
                    operandNum = Random.Range(0, 2);
                    if (operandNum == 0) new_expr.operand1 = advancedVal;
                    else new_expr.operand2 = advancedVal;
                    break;
                case operation.Division:
                    while (advancedNum > 100)
                    {
                        advancedVal = GenerateAdvancedOperands();
                        advancedNum = SolveOperand(advancedVal);
                    }
                    var operand2 = Random.Range(1, 100);
                    var product = advancedNum * operand2;
                    new_expr.operand1 = new Operand(product, operation.None);
                    new_expr.operand2 = advancedVal;
                    break;
                case operation.Modulus:
                    if (advancedNum > expr.operand2.operand) new_expr.operand1 = advancedVal;
                    else new_expr.operand2 = advancedVal;
                    break;
            }

            expression_list[i++] = new_expr;

        }
        
        return expression_list;
    }

    private static Operand GenerateAdvancedOperands()
    {
        var op = (operation)Random.Range(5, 9);

        return op switch
        {
            operation.SquareRoot => 
                new Operand((int)Mathf.Pow(Random.Range(1, 17), 2), operation.SquareRoot),
            operation.Squared => 
                new Operand(Random.Range(1, 17), operation.Squared),
            operation.Log2 => 
                new Operand((int)Mathf.Pow(2, Random.Range(1, 10)), operation.Log2),
            operation.Cubed => 
                new Operand(Random.Range(1, 10), operation.Cubed),
            _ => new Operand(-1, operation.None)
        };
    }

    private static int SolveOperand(Operand operand)
    {
        var op = operand.op;
        var num = operand.operand;
        
        return op switch
        {
            operation.SquareRoot => (int)Mathf.Sqrt(num),
            operation.Squared => (int)Mathf.Pow(num, 2),
            operation.Log2 => (int)Mathf.Log(num, 2),
            operation.Cubed => (int)Mathf.Pow(num, 3),
            operation.Fibonacci => Fibonacci(num),
            operation.Factorial => Factorial(num),
            _ => num
        };
    }

    private static int SolveExpression(MathExpression expr)
    {
        var operand1 = SolveOperand(expr.operand1);
        var operand2 = SolveOperand(expr.operand2);

        return expr.op switch
        {
            operation.Addition => operand1 + operand2,
            operation.Subtraction => operand1 - operand2,
            operation.Multiplication => operand1 * operand2,
            operation.Division => operand1 / operand2,
            operation.Modulus => operand1 % operand2,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static bool CheckAnswer(MathExpression expr, int answer)
    {
        return SolveExpression(expr) == answer;
    }
    
    private static MathExpression GetBonusQuestion()
    {
        var fib = new Operand(Random.Range(1,16), operation.Fibonacci);
        var fac = new Operand(Random.Range(1, 7), operation.Factorial);
        if (SolveOperand(fib) < 100 && SolveOperand(fac) < 100)
        {
            return new MathExpression(fib, operation.Multiplication, fac);
        }

        return Random.Range(1, 3) == 1 
            ? new MathExpression(fib, operation.Addition, fac) 
            : new MathExpression(fib, operation.Subtraction, fac);
    }

    private static int Fibonacci(int index)
    {
        if (index is 0 or 1)
        {
            return 1;
        }
        return Fibonacci(index - 1) + Fibonacci(index - 2);
    }

    private static int Factorial(int num)
    {
        if (num is 0 or 1)
        {
            return 1;
        }
        return num * Factorial(num - 1);
    }


    public static MathExpression[] GetMathProblems()
    {
        var easyQuestions = GetEasyExpressions();
        var hardQuestions = GetHardExpressions();
        var bonusQuestion = GetBonusQuestion();

        var questions = new MathExpression[11];
        for (int i = 0; i < 5; i++)
        {
            questions[i] = easyQuestions[i];
        }
        for (int i = 0; i < 5; i++)
        {
            questions[i + 5] = hardQuestions[i];
        }

        questions[10] = bonusQuestion;
        
        return questions;
    }
}
