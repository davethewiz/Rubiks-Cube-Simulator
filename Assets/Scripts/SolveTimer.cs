using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolveTimer : MonoBehaviour {
    [SerializeField] private RubiksCube rubiksCube;

    [SerializeField] private GameObject playButton;

    [SerializeField] private Text timerText;
    [SerializeField] private Color solvedColor;
    [SerializeField] private GameObject solvedText;

    [SerializeField] private float timeElapsed;

    [SerializeField] private int scrambleMoves;

	void Start () {
        timeElapsed = 0;
    }
	
	void Update () {
        if (!rubiksCube.DisableInput && !rubiksCube.Solved) {
            timeElapsed += Time.deltaTime;

            float minutes = Mathf.Floor(timeElapsed / 60);
            string addMinutesZero = minutes < 10 ? "0" : "";
            string minutesText = addMinutesZero + minutes.ToString();

            float seconds = Mathf.Floor(timeElapsed) - minutes * 60;
            string addSecondsZero = seconds < 10 ? "0" : "";
            string secondsText = addSecondsZero + seconds.ToString();

            float milliseconds = timeElapsed - Mathf.Floor(timeElapsed);
            string millisecondsText = milliseconds.ToString().Substring(1, 3);

            timerText.text = minutesText + ":" + secondsText + millisecondsText;
        }
        else {
            if (rubiksCube.Solved && timeElapsed > 0) {
                timerText.color = solvedColor;
                solvedText.SetActive(true);
            }
        }
	}

    public void StartSolve() {
        rubiksCube.Scramble(scrambleMoves);
        playButton.SetActive(false);
        timerText.text = "Scrambling";
    }
}
