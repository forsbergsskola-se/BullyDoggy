using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

[Serializable]
public class Game : MonoBehaviour
{
    public State currentState;

    public GameView view;

    private void OnEnable()
    {
        view.UpdateView(currentState);
    }

    State CurrentState
    {
        set
        {
            currentState = value;
            view.UpdateView(currentState);
        }
    }

    void Update()
    {
        
        if(currentState.IsEnemyWinState() || currentState.IsPlayerWinState())
            return;
        
        if (currentState.playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentState.player.x > 0)
            {
                CurrentState = new State()
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.left,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }

            if (Input.GetKeyDown(KeyCode.S) && currentState.player.y > 0)
            {
                CurrentState = new State()
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.down,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }

            if (Input.GetKeyDown(KeyCode.D) && currentState.player.x < currentState.gridWidth - 1)
            {
                CurrentState = new State()
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.right,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }

            if (Input.GetKeyDown(KeyCode.W) && currentState.player.y < currentState.gridHeight - 1)
            {
                CurrentState = new State()
                {
                    grid = currentState.grid,
                    gridWidth = currentState.gridWidth,
                    player = currentState.player + Vector2Int.up,
                    enemy = currentState.enemy,
                    goal = currentState.goal,
                    playerTurn = !currentState.playerTurn
                };
            }
            
        }
        else
        {
            var enemyWinPath = find_path(currentState);
            CurrentState = enemyWinPath[1];

        }
    }

    State[] find_path(State start_node)
    {
        int depth = 1;
        while (depth < 10000)
        {
            State[] result = find_path(start_node, depth++);
            if (result != null)
                return result;
        }

        throw new Exception("Found no path!");
    }

    State[] find_path(State start_node, int depth)
    {
        HashSet<State> visited_noedes = new HashSet<State>();
        visited_noedes.Add(start_node);

        Stack<State> path = new Stack<State>();
        path.Push(start_node);

        while (path.Count > 0)
        {
            bool found_next_node = false;
            foreach (var neighbour in path.Peek().GetAdjacents())
            {
                if (visited_noedes.Contains(neighbour))
                    continue;
                if (depth > 0)
                {
                    visited_noedes.Add(neighbour);
                    path.Push(neighbour);
                    depth--;
                    if (neighbour.IsEnemyWinState())
                    {
                        return path.Reverse().ToArray();
                    }

                    found_next_node = true;
                    break;
                }
            }

            if (!found_next_node)
            {
                path.Pop();
                depth++;
            }
        }

        return null;
    }
    
}
