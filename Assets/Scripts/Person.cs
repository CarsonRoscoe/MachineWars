using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Person : IObjectType {
    int totalFrames = 240;
    public double currentFrames = 600;
    Rigidbody2D rigidBody;

    float _moveThisFrame = 0;
    float _rotateThisFrame = 0;
    bool _moved = false;
    float moveThisFrame {
        get {
            return _moveThisFrame;
        }
        set {
            if (_moveThisFrame < 15 && _moveThisFrame > -10)
                _moveThisFrame = value;
        }
    }
    float rotateThisFrame {
        get {
            return _rotateThisFrame;
        }
        set {
            if (_rotateThisFrame < 1 && _rotateThisFrame > -1)
                _rotateThisFrame = value;
        }
    }

    private int _score;
    public int Score {
        get {
            return _score;
        }
        set {
            _score = value;
        }
    }

    void Update() {
        if (currentFrames-- == 0) {
            Killed();
        }
        if (currentFrames % 300 == 0) {
            Score++;
        }

        if (rotateThisFrame != 0)
        transform.Rotate(0, 0, rotateThisFrame);
        if (moveThisFrame != 0)
            rigidBody.AddForce(transform.right * moveThisFrame);

        _rotateThisFrame = 0;
        _moveThisFrame = 0;
    }

    void Start() {
        _score = 0;
        Type = ObjectType.Enemy;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    #region Outputs
    protected void RotateRight() {
        if (gameObject == null)
            return;
        _rotateThisFrame += 1;
    }

    protected void RotateLeft() {
        if (gameObject == null)
            return;
        _rotateThisFrame -= 1;
    }

    protected void MoveForwards() {
        if (gameObject == null)
            return;
        _moveThisFrame += 12;
    }

    protected void MoveBackwards() {
        if (gameObject == null)
            return;
        _moveThisFrame -= 8;
    }

    protected void Attack() {
        if (gameObject == null)
            return;
        gameObject.GetComponentInChildren<Sword>().Attack();
    }
    #endregion

    public Action[] PossibleReactions() {
        return new Action[] { RotateRight, RotateLeft, MoveForwards, MoveBackwards, Attack };
    }

    public void Killed() {
        Genome[] genome = new Genome[5];
        var whiskers = GetComponents<Whisker>();

        if (whiskers.Length == 0)
            return;

        for (int i = 0; i < whiskers.Length; i++) {
            genome[i] = whiskers[i].Genome;
        }

        genome = genome.OrderBy(x => x.WhiskerGenome).ToArray();
        if (_moved)
            Score += 1;
        PersonFactory.instance.RecyclePerson(genome, Score);
        Destroy(gameObject);
    }
}
