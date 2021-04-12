using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void EscAction();

    public static event EscAction EscClicked;
    
    public enum GameState { Play, Building, Menu}

    public GameState gameState;
    
    public LayerMask layerMask;
    
    public PathFinding pathFinding;

    public Builder builder;

    public Grid grid;
    
    
    
    [Tooltip("node with the selected unit")]
    public Node startNode;

    [Tooltip("product type of selected unit")]
    public AbstractUnit selectedUnit;
    
    [Tooltip("product type of selected structure")]
    public Production selectedBuild;

    #region Input_Fields
    
    public bool MouseDown => Input.GetMouseButtonDown(0);
    
    public bool MouseUp => Input.GetMouseButtonUp(0);

    public bool MousePressed => Input.GetMouseButton(0);
    public bool KeyDownB => Input.GetKeyDown(KeyCode.B);
    public bool KeyDownESC => Input.GetKeyDown(KeyCode.Escape);
    
    #endregion

    
    #region Unity_Methods

    private void Awake()
    {
        Instance = this;

        pathFinding = GetComponent<PathFinding>();
        builder = GetComponent<Builder>();
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (KeyDownESC && EscClicked != null)
        {
            EscClicked();
            gameState = GameState.Play;

        }

        if (KeyDownB) BuildMaker(selectedBuild);

    }
    
    #endregion

    #region GameManager_Methods

    /// <summary>
    /// The structure in the UI creates the official build.
    /// </summary>
    /// <param name="production"></param>
    [ContextMenu("Build")]
    public void BuildMaker(Production production)
    {
        gameState = GameState.Building;
        selectedBuild = production;
        builder.BuildCreater(GameObjectHelper.GetMouseWorldPosition(), production, grid.CellSize());
    }

    #endregion

}
