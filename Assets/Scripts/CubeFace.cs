using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeFace : MonoBehaviour {
    [SerializeField] private RubiksCube rubiksCube;

    private Tween turnTween;
    public bool Turning => turnTween != null && turnTween.active;

    private List<GameObject> startGroup = new List<GameObject>();

    public bool Solved {
        get {
            foreach (GameObject obj in GetSurroundingCubes()) {
                if (!startGroup.Contains(obj))
                    return false;
            }
            return true;
        }
    }

    void Start() {
        rubiksCube.AddFace(this);
        startGroup = GetSurroundingCubes();
    }

    private List<GameObject> GetSurroundingCubes() {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(transform.position, boxCollider.size / 2, transform.rotation);

        List<GameObject> surroundingCubes = new List<GameObject>();

        foreach (Collider col in colliders) {
            if (col.tag == "moveable")
                surroundingCubes.Add(col.gameObject);
        }

        return surroundingCubes;
    }

    private void ParentSurroundingCubes() {
        foreach (GameObject obj in GetSurroundingCubes()) {
            obj.transform.parent = transform;
        }
    }

    private void Turn(bool clockwise) {
        ParentSurroundingCubes();

        int dir = clockwise ? 1 : -1;
        Quaternion newRot = Quaternion.AngleAxis(90 * dir, transform.forward) * transform.rotation;

        turnTween = transform.DORotateQuaternion(newRot, 0.25f);
    }

    public void TurnClockWise() => Turn(true);

    public void TurnCounterClockwise() => Turn(false);

    void OnMouseOver() {
        if (!rubiksCube.DisableInput && !rubiksCube.Turning) {
            if (Input.GetMouseButtonDown(0))
                Turn(true);
            else if (Input.GetMouseButtonDown(1))
                Turn(false);
        }
    }
}
