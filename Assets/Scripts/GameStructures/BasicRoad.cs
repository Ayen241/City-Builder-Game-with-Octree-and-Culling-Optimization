using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class BasicRoad : BasicBuilding
    {
        [System.Serializable]
        public struct RoadTiles
        {
            public GameObject straight;
            public GameObject turn;
            public GameObject threeWay;
            public GameObject fourWay;
        }

        public RoadTiles roadTiles;
        public RoadPoint[] RoadPoints = null;

        private bool isRoadPointContained(RoadPoint point)
        {
            for (int i = 0; i < RoadPoints.Length; i++)
            {
                for (int p = 0; p < RoadPoints[i].connectedPoints.Length; p++)
                    if (RoadPoints[i].connectedPoints[p].gameObject.Equals(point))
                        return true;
            }
            return false;
        }

        public override void OnBuild()
        {
            base.RefreshVisuals();
            RefreshVisualsAndChainOnce();
        }

        public override void OnErase()
        {
            // Debug log for tracing erasing action
            Debug.Log($"Erasing road at position: {transform.position}");

            // Ensure collider is properly managed during erasing
            BoxCollider collider = GetComponentInChildren<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = false;
                Debug.Log("Disabled BoxCollider on erase.");
            }

            // Refresh visuals and update road connections
            RefreshVisualsAndChainOnce();
            base.OnErase();
        }

        public override void RefreshVisuals()
        {
            base.RefreshVisuals();
            RefreshTileVisual();
            RefreshRoadPoints();
        }

        public override void RefreshVisualsAndChainOnce()
        {
            RefreshTileVisual();
            UpdateTileConnections();
            RefreshRoadPoints();
        }

        private void UpdateTileConnections()
        {
            var neighbors = Neighbors.GetNeighbors(Location.All, LayerMask.GetMask("Buildings", "Roads"));
            if (neighbors != null)
            {
                for (int i = 0; i < neighbors.Length; i++)
                {
                    BasicRoad road = neighbors[i] as BasicRoad;
                    if (road != null)
                        road.RefreshVisuals();
                    else
                        neighbors[i].RefreshVisuals();
                }
            }
        }

        private void RefreshTileVisual()
        {
            roadTiles.straight.SetActive(false);
            roadTiles.turn.SetActive(false);
            roadTiles.threeWay.SetActive(false);
            roadTiles.fourWay.SetActive(false);

            int layer = LayerMask.NameToLayer("Roads");
            switch (Neighbors.GetNumberOccupiedSidesByLayer(LayerMask.GetMask("Roads")))
            {
                case 1:
                    roadTiles.straight.SetActive(true);
                    RoadPoints = roadTiles.straight.GetComponentsInChildren<RoadPoint>();
                    SetRotationForSingleConnection(layer);
                    break;
                case 2:
                    if (IsStraightRoad(layer))
                    {
                        roadTiles.straight.SetActive(true);
                        RoadPoints = roadTiles.straight.GetComponentsInChildren<RoadPoint>();
                        SetRotationForStraightRoad(layer);
                    }
                    else
                    {
                        roadTiles.turn.SetActive(true);
                        RoadPoints = roadTiles.turn.GetComponentsInChildren<RoadPoint>();
                        SetRotationForTurnRoad(layer);
                    }
                    break;
                case 3:
                    roadTiles.threeWay.SetActive(true);
                    RoadPoints = roadTiles.threeWay.GetComponentsInChildren<RoadPoint>();
                    SetRotationForThreeWayRoad(layer);
                    break;
                case 4:
                    roadTiles.fourWay.SetActive(true);
                    RoadPoints = roadTiles.fourWay.GetComponentsInChildren<RoadPoint>();
                    transform.rotation = Quaternion.identity;
                    break;
                default:
                    roadTiles.straight.SetActive(true);
                    RoadPoints = roadTiles.straight.GetComponentsInChildren<RoadPoint>();
                    transform.rotation = Quaternion.identity;
                    break;
            }
        }

        private bool IsStraightRoad(int layer)
        {
            return (Neighbors.IsContainsLayer(layer, Location.Front) && Neighbors.IsContainsLayer(layer, Location.Back)) ||
                   (Neighbors.IsContainsLayer(layer, Location.Left) && Neighbors.IsContainsLayer(layer, Location.Right));
        }

        private void SetRotationForSingleConnection(int layer)
        {
            if (Neighbors.IsContainsLayer(layer, Location.Front))
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            else if (Neighbors.IsContainsLayer(layer, Location.Right))
                transform.rotation = Quaternion.LookRotation(Vector3.right);
            else if (Neighbors.IsContainsLayer(layer, Location.Back))
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            else // Neighbors.IsContainsLayer(layer, Location.Left)
                transform.rotation = Quaternion.LookRotation(Vector3.left);
        }

        private void SetRotationForStraightRoad(int layer)
        {
            if (Neighbors.IsContainsLayer(layer, Location.Front) && Neighbors.IsContainsLayer(layer, Location.Back))
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            else if (Neighbors.IsContainsLayer(layer, Location.Left) && Neighbors.IsContainsLayer(layer, Location.Right))
                transform.rotation = Quaternion.LookRotation(Vector3.right);
        }

        private void SetRotationForTurnRoad(int layer)
        {
            if (Neighbors.IsContainsLayer(layer, Location.Front) && Neighbors.IsContainsLayer(layer, Location.Left))
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            else if (Neighbors.IsContainsLayer(layer, Location.Front) && Neighbors.IsContainsLayer(layer, Location.Right))
                transform.rotation = Quaternion.LookRotation(Vector3.right);
            else if (Neighbors.IsContainsLayer(layer, Location.Back) && Neighbors.IsContainsLayer(layer, Location.Right))
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            else // Neighbors.IsContainsLayer(layer, Location.Back) && Neighbors.IsContainsLayer(layer, Location.Left)
                transform.rotation = Quaternion.LookRotation(Vector3.left);
        }

        private void SetRotationForThreeWayRoad(int layer)
        {
            if (Neighbors.IsContainsLayer(layer, Location.Left) && Neighbors.IsContainsLayer(layer, Location.Front) && Neighbors.IsContainsLayer(layer, Location.Right))
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            else if (Neighbors.IsContainsLayer(layer, Location.Front) && Neighbors.IsContainsLayer(layer, Location.Right) && Neighbors.IsContainsLayer(layer, Location.Back))
                transform.rotation = Quaternion.LookRotation(Vector3.right);
            else if (Neighbors.IsContainsLayer(layer, Location.Right) && Neighbors.IsContainsLayer(layer, Location.Back) && Neighbors.IsContainsLayer(layer, Location.Left))
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            else // Neighbors.IsContainsLayer(layer, Location.Back) && Neighbors.IsContainsLayer(layer, Location.Left) & Neighbors.IsContainsLayer(layer, Location.Front)
                transform.rotation = Quaternion.LookRotation(Vector3.left);
        }

        private RoadPoint[] GetAllNearestRoadPoints()
        {
            var neighborRoads = Neighbors.GetNeighbors(Location.All, LayerMask.GetMask("Roads"));
            if (neighborRoads != null)
            {
                var result = new List<RoadPoint>();
                for (int indexNeighborRoad = 0; indexNeighborRoad < neighborRoads.Length; indexNeighborRoad++)
                {
                    BasicRoad road = neighborRoads[indexNeighborRoad] as BasicRoad;
                    if (road != null && road.RoadPoints != null)
                    {
                        result.AddRange(road.RoadPoints);
                    }
                }
                return result.Count > 0 ? result.ToArray() : null;
            }
            return null;
        }

        public RoadPoint GetClosestPoint(Vector3 position, float maxSqrDist)
        {
            return GetClosestPoint(RoadPoints, position, maxSqrDist);
        }

        public RoadPoint GetClosestPoint(Vector3 position, RoadPoint excludedPoint, float maxSqrDist)
        {
            return GetClosestPoint(RoadPoints, position, excludedPoint, maxSqrDist);
        }

        private RoadPoint GetClosestPoint(RoadPoint[] roadPoints, Vector3 closestTo, float maxSqrDist = 0.25f)
        {
            float minSqrDist = float.MaxValue;
            RoadPoint closestPoint = null;

            foreach (var roadPoint in roadPoints)
            {
                float sqrDist = (closestTo - roadPoint.transform.position).sqrMagnitude;
                if (sqrDist <= maxSqrDist && sqrDist < minSqrDist)
                {
                    minSqrDist = sqrDist;
                    closestPoint = roadPoint;
                }
            }

            return closestPoint;
        }

        private RoadPoint GetClosestPoint(RoadPoint[] roadPoints, Vector3 closestTo, RoadPoint excludedPoint, float maxSqrDist = 0.25f)
        {
            float minSqrDist = float.MaxValue;
            RoadPoint closestPoint = null;

            foreach (var roadPoint in roadPoints)
            {
                if (roadPoint == excludedPoint) continue;

                float sqrDist = (closestTo - roadPoint.transform.position).sqrMagnitude;
                if (sqrDist <= maxSqrDist && sqrDist < minSqrDist)
                {
                    minSqrDist = sqrDist;
                    closestPoint = roadPoint;
                }
            }

            return closestPoint;
        }

        private void RefreshRoadPoints()
        {
            if (RoadPoints == null) return;

            var nearestPoints = GetAllNearestRoadPoints();
            if (nearestPoints == null) return;

            foreach (var roadPoint in RoadPoints)
            {
                var closestPoint = GetClosestPoint(nearestPoints, roadPoint.transform.position);
                if (closestPoint != null)
                {
                    if (roadPoint.state == RoadPoint.State.Out && closestPoint.state == RoadPoint.State.In)
                    {
                        roadPoint.connectedPoints = new RoadPoint[] { closestPoint };
                    }
                    else if (roadPoint.state == RoadPoint.State.In && closestPoint.state == RoadPoint.State.Out)
                    {
                        closestPoint.connectedPoints = new RoadPoint[] { roadPoint };
                    }
                }
            }
        }
    }
}
