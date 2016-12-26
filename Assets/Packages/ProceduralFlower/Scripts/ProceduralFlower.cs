﻿using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace mattatz.ProceduralFlower {

    public class ProceduralFlower : MonoBehaviour {

        List<Vector3> points = new List<Vector3>();
        [SerializeField, Range(137.4f, 137.6f)] float alpha = 137.5f;
        [SerializeField] float size = 0.04f;
        [SerializeField] float c = 0.1f;
        [SerializeField] float n = 100;
        [SerializeField] GameObject petal;

        void Start () {
            var floret = new Florets();

            var inv = 1f / n;
            for(int i = 0; i < n; i++) {
                var r = i * inv;

                // points.Add(floret.Get(i, c));
                var p = floret.Get(i + 1, c);

                var go = Instantiate(petal) as GameObject;
                go.transform.SetParent(transform, false);
                // go.transform.localScale = Vector3.one * Mathf.Clamp01(r + 0.1f);
                go.transform.localScale = Vector3.one * Mathf.Clamp01(p.magnitude + 0.1f);
                go.transform.localPosition = p + Vector3.up * (1f - r) * 0.25f;
                go.transform.localRotation = Quaternion.LookRotation(Vector3.up, p.normalized) * Quaternion.AngleAxis((1f - r) * 60f + 1f, Vector3.right);
            }

            petal.SetActive(false);
        }

        void OnDrawGizmos () {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            points.ForEach(p => {
                Gizmos.DrawSphere(p, size);
            });

        }

    }

    public class Florets {

        const float ANGLE = 137.5f * Mathf.Deg2Rad;

        // Vogel's formula : Generate patterns of florets 
        // n is the ordering number of a floret, counting outward from the center.
        // phi is the angle between a reference direction and the position vector of the nth floret in a polar coordinate system originating at the center of the capitulum.
        // r is the distance between the center of the capitulum and the center of the nth floret, given a constant scaling parameter c.
        public Vector3 Get (int n, float c, float alpha = 137.5f) {
            // var phi = n * ANGLE;
            var phi = n * alpha * Mathf.Deg2Rad;
            var r = c * Mathf.Sqrt(n);
            return new Vector3(Mathf.Cos(phi) * r, 0f, Mathf.Sin(phi) * r);
        }

    }

}

