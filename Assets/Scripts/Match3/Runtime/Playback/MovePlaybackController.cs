using System;
using System.Collections;
using System.Collections.Generic;
using Match3.Model.Data;
using Match3.Runtime.Input;
using Match3.Runtime.Presentation;
using UnityEngine;

namespace Match3.Runtime.Playback
{
    public class MovePlaybackController : MonoBehaviour
    {
        [SerializeField] float swapDuration = .2f;
        [SerializeField] float clearDuration = .2f;
        [SerializeField] float gravityDuration = .2f;
        [SerializeField] float spawnDuration = .2f;

        BoardView boardView;
        SwapInputController input;

        bool isPlaying;

        public event Action onFinished;

        public void Initialize(BoardView view, SwapInputController input)
        {
            boardView = view;
            this.input = input;
        }

        public void Play(MoveProcessResult result)
        {
            if (isPlaying) return;
            if (result == null) return;

            StartCoroutine(PlaybackRoutine(result));
        }

        IEnumerator PlaybackRoutine(MoveProcessResult result)
        {
            isPlaying = true;
            input.DisableInput();

            if (result.IsSuccess)
                yield return SwapRoutine(result.SwapResult.From, result.SwapResult.To);

            if (result.CascadeSteps != null)
            {
                foreach (var step in result.CascadeSteps)
                {
                    if (step.Clears != null && step.Clears.Count > 0)
                        yield return ClearRoutine(step.Clears);

                    if (step.GravityMoves != null && step.GravityMoves.Count > 0)
                        yield return GravityRoutine(step.GravityMoves);

                    if (step.Spawns != null && step.Spawns.Count > 0)
                        yield return SpawnRoutine(step.Spawns);
                }
            }

            isPlaying = false;
            input.EnableInput();
            onFinished?.Invoke();
        }

        public void PlayInvalidSwapBounce(Position a, Position b)
        {
            if (isPlaying) return;
            StartCoroutine(InvalidSwapRoutine(a, b));
        }

        IEnumerator InvalidSwapRoutine(Position aPos, Position bPos)
        {
            isPlaying = true;
            input.DisableInput();

            TileView a = boardView.GetTileView(aPos);
            TileView b = boardView.GetTileView(bPos);

            if (a != null && b != null)
                yield return StartCoroutine(SwapBounceRoutine(a, b));

            isPlaying = false;
            input.EnableInput();
            onFinished?.Invoke();
        }

        IEnumerator SwapBounceRoutine(TileView a, TileView b)
        {
            Vector3 startA = a.transform.position;
            Vector3 startB = b.transform.position;

            float half = swapDuration * 0.5f;
            float t = 0f;

            while (t < half)
            {
                if (a == null || b == null) yield break;

                t += Time.deltaTime;
                float k = t / half;

                a.transform.position = Vector3.Lerp(startA, startB, k);
                b.transform.position = Vector3.Lerp(startB, startA, k);

                yield return null;
            }

            t = 0f;

            while (t < half)
            {
                if (a == null || b == null) yield break;

                t += Time.deltaTime;
                float k = t / half;

                a.transform.position = Vector3.Lerp(startB, startA, k);
                b.transform.position = Vector3.Lerp(startA, startB, k);

                yield return null;
            }

            a.transform.position = startA;
            b.transform.position = startB;
        }

        IEnumerator SwapRoutine(Position from, Position to)
        {
            TileView a = boardView.GetTileView(from);
            TileView b = boardView.GetTileView(to);

            if (a == null || b == null) yield break;

            Vector3 startA = a.transform.position;
            Vector3 startB = b.transform.position;
            float time = 0f;

            while (time < swapDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / swapDuration);

                a.transform.position = Vector3.Lerp(startA, startB, t);
                b.transform.position = Vector3.Lerp(startB, startA, t);

                yield return null;
            }

            a.transform.position = startB;
            b.transform.position = startA;

            boardView.SwapTileReferences(from, to);

            a.SetPosition(to);
            b.SetPosition(from);
        }

        IEnumerator ClearRoutine(List<ClearResult> clears)
        {
            List<TileView> views = new List<TileView>();

            foreach (var c in clears)
            {
                TileView tv = boardView.GetTileView(c.Position);
                if (tv == null) continue;
                boardView.RemoveTileView(c.Position);  
                tv.transform.localScale = Vector3.one; 
                views.Add(tv);
            }

            float time = 0f;
            while (time < clearDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / clearDuration);
                foreach (var tv in views)
                {
                    if (tv == null) continue;
                    tv.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
                }
                yield return null;
            }

            foreach (var tv in views)
            {
                if (tv != null)
                    Destroy(tv.gameObject);
            }
        }


       IEnumerator GravityRoutine(List<MoveResult> moves)
        {
            Dictionary<TileView, Vector3> start = new();
            Dictionary<TileView, Vector3> end = new();

            foreach (var m in moves)
            {
                TileView tv = boardView.GetTileView(m.From);
                if (tv == null) continue;

                if (start.ContainsKey(tv)) continue;

                start[tv] = tv.transform.position;
                end[tv] = boardView.PositionToWorld(m.To);
            }

            float time = 0f;

            while (time < gravityDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / gravityDuration);

                foreach (var kv in start)
                {
                    if (kv.Key == null) continue;

                    kv.Key.transform.position =
                        Vector3.Lerp(kv.Value, end[kv.Key], t);
                }

                yield return null;
            }

            boardView.ApplyMoveReferences(moves);

            foreach (var m in moves)
            {
                TileView tv = boardView.GetTileView(m.To);
                if (tv == null) continue;

                tv.SetPosition(m.To);
                tv.transform.position = boardView.PositionToWorld(m.To);
            }
        }

        IEnumerator SpawnRoutine(List<SpawnResult> spawns)
        {
            List<TileView> tiles = new List<TileView>();
            Dictionary<TileView, Vector3> startPos = new();
            Dictionary<TileView, Vector3> endPos = new();

            foreach (var s in spawns)
            {
                TileView tv = boardView.CreateTileView(s.Position, s.TileType);
                if (tv == null) continue;

                Vector3 end = boardView.PositionToWorld(s.Position);
                Vector3 start = end + Vector3.up * 2f;

                tv.transform.position = start;
                tv.transform.localScale = Vector3.one;

                tiles.Add(tv);
                startPos[tv] = start;
                endPos[tv] = end;
            }

            float time = 0f;

            while (time < spawnDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / spawnDuration);

                foreach (var tv in tiles)
                {
                    if (tv == null) continue;

                    tv.transform.position =
                        Vector3.Lerp(startPos[tv], endPos[tv], t);
                }

                yield return null;
            }

            foreach (var tv in tiles)
            {
                if (tv == null) continue;
                tv.transform.position = endPos[tv];
            }
        }

    }
}