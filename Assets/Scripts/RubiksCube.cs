using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCube : MonoBehaviour {

    public bool DisableInput { get; private set; }

    private List<CubeFace> faces = new List<CubeFace>();

    public bool Turning {
        get {
            foreach (CubeFace face in faces) {
                if (face.Turning)
                    return true;
            }
            return false;
        }
    }

    public bool Solved {
        get {
            foreach (CubeFace face in faces) {
                if (!face.Solved)
                    return false;
            }
            return true;
        }
    }

    private void Update() {
        if (!Turning && Solved)
            DisableInput = true;
    }

    public void AddFace(CubeFace newFace) {
        faces.Add(newFace);
    }

    private IEnumerator ScrambleRoutine(int moves) {
        DisableInput = true;

        for (int i = 0; i < moves; i++) {
            int randomIndex = Random.Range(0, 6);
            faces[randomIndex].TurnClockWise();
            yield return new WaitUntil(() => !Turning);
        }

        DisableInput = false;
        yield return null;
    }

    public void Scramble(int moves) {
        StartCoroutine(ScrambleRoutine(moves));
    }
}