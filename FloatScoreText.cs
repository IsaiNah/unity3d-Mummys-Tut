using System;
using TMPro;
using UnityEngine;



    public class FloatScoreText : MonoBehaviour
    {
     [SerializeField]   private float _floatSpeed = 5.0f;

     
        public void SetScoreValue(int scoreMultipler)
        {
            var text = GetComponent<TMP_Text>();
            text.SetText("x " + scoreMultipler);
            
            GetComponent<TMP_Text>().SetText("x " + scoreMultipler);
            
            if (scoreMultipler < 3)
                text.color = Color.white;
            else if (scoreMultipler < 10)
                text.color = Color.green;
            else if (scoreMultipler < 20)
                text.color = Color.yellow;
            else if (scoreMultipler < 30)
                text.color = Color.red;
            
            Destroy(gameObject, 5.0f);
        }

        private void Update()
        {
            transform.position += transform.up * Time.deltaTime * _floatSpeed;
        }
    }
