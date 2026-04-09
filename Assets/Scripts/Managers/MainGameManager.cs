using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static MathProblems;
using static GlobalScript;
using Image = UnityEngine.UI.Image;

namespace Managers
{
    public class MainGameManager : MonoBehaviour
    {
        
        private MathExpression[] GameQuestions;

        [SerializeField] private GameObject assignment_m;
        [SerializeField] private GameObject assignment_l;

        [SerializeField] private GameObject points;
        
        private void Start()
        {
            GameQuestions = GetMathProblems();
            SetMathQuestions();
            SetLanguageQuestions();
        }
        
        private void SetMathQuestions()
        {
            var questions = assignment_m.transform.Find("Questions");

            for (var i = 0; i < questions.childCount; i++)
            {
                var expression = GameQuestions[i];
                var question = questions.GetChild(i);
                for (var j = 0; j < question.childCount; j++)
                {
                    var child = question.GetChild(j);
                    var preOp = operation.None;
                    switch (child.name)
                    {
                        case "Operand1":
                            child.GetComponent<TextMeshProUGUI>().text = expression.operand1.operand.ToString();
                            break;
                        case "Operand2":
                            child.GetComponent<TextMeshProUGUI>().text = expression.operand2.operand.ToString();
                            break;
                        case "Operator":
                            child.GetComponent<Image>().sprite = iconDictionary[expression.op];
                            break;
                        case "PreOperator1":
                            preOp = expression.operand1.op;
                            if (preOp != operation.None)
                            {
                                child.gameObject.SetActive(true);
                                child.GetComponent<Image>().sprite = iconDictionary[preOp];
                            }
                            break;
                        case "PreOperator2":
                            preOp = expression.operand2.op;
                            if (preOp != operation.None)
                            {
                                child.gameObject.SetActive(true);
                                child.GetComponent<Image>().sprite = iconDictionary[preOp];
                            }
                            break;
                    }
                }
            }
        }

        private void SetLanguageQuestions()
        {
            var questions = assignment_l.transform.Find("Questions");

            for (var i = 0; i < questions.childCount; i++)
            {
                var question = questions.GetChild(i);
                var text = question.GetComponent<TextMeshProUGUI>();
                text.text = wordDictionary.Values.ToArray()[i];
            }
        }

        private int CheckAnswers()
        {
            var answers_m = assignment_m.transform.Find("Answers");
            var answers_l = assignment_l.transform.Find("Answers");

            var num_points = 0;
            
            for (var i = 0; i < answers_m.childCount; i++)
            {
                var answer_object = answers_m.GetChild(i);
                var answer = answer_object.GetComponent<TMP_InputField>().text;
                if (int.TryParse(answer, out var num) && MathProblems.CheckAnswer(GameQuestions[i], num))
                {
                    num_points++;
                }
            }

            for (var i = 0; i < answers_l.childCount; i++)
            {
                var answer_object = answers_l.GetChild(i);
                var answer = answer_object.GetComponent<TMP_InputField>().text;
                var true_answer = wordDictionary.Keys.ToArray()[i];
                print(answer);
                print(true_answer);
                if (answer.ToLower().Equals(true_answer.ToLower())) { num_points++; }
            }
            
            return num_points;
        }

        public void Submit()
        {
            var score = CheckAnswers();
            points.transform.Find("PlayerScore").GetComponent<TextMeshProUGUI>().text = score.ToString();
            points.SetActive(true);
            if (score >= 20)
            {
                points.transform.Find("PassingMessage").gameObject.SetActive(true);
            }
        }
    }
}
