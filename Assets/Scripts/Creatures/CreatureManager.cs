using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour {

    public static QuadTree<Creature> QuadTree { get { return quadTree; } }

    private static QuadTree<Creature> quadTree;

    private void Awake()
    {
        quadTree = new QuadTree<Creature>(2048);
    }
    private void Update()
    {
        quadTree.Clear();
    }
    private void LateUpdate()
    {
        //quadTree.DrawDebug();
    }
    public static List<Creature> GetCreatures(Vector3 point, float range)
    {
        List<Creature> creatures = QuadTree.GetNearbyObjects(point, Vector2.one * range);

        return new List<Creature>(creatures.Where(x => Vector2.Distance(point, x.transform.position) <= range));
    }
    public static void Add(Creature creature)
    {
        quadTree.Add(creature, creature.Rect);
    }
}
