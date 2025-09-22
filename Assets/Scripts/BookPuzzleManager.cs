using UnityEngine;
using System.Collections.Generic;

public class BookPuzzleManager : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public PuzzleCube[] cubes; // Correct order defined in Inspector OR by bookCount
    public Transform drawer;   // The drawer to move
    public Vector3 drawerOpenPosition = new Vector3(4.63f, 1.599501f, 11.9f);

    private List<PuzzleCube> clickedCubes = new List<PuzzleCube>();

    void Start()
    {
        // Optional: sort cubes if order depends on bookCount
        System.Array.Sort(cubes, (a, b) => a.bookCount.CompareTo(b.bookCount));
    }

    public void CubeClicked(PuzzleCube clickedCube)
    {
        // Prevent clicking same cube twice
        if (!clickedCubes.Contains(clickedCube))
        {
            clickedCube.ClickCube();
            clickedCubes.Add(clickedCube);

            // When 3 cubes have been clicked, check sequence
            if (clickedCubes.Count == cubes.Length)
            {
                CheckSequence();
            }
        }
    }

    void CheckSequence()
    {
        bool correct = true;
        for (int i = 0; i < cubes.Length; i++)
        {
            if (clickedCubes[i] != cubes[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            OpenDrawer();
        }
        else
        {
            ResetPuzzle();
        }
    }

    void ResetPuzzle()
    {
        Debug.Log("Wrong sequence! Resetting puzzle...");

        foreach (var cube in cubes)
        {
            cube.ResetCube();
        }
        clickedCubes.Clear();
    }

    void OpenDrawer()
    {
        if (drawer != null)
        {
            drawer.position = drawerOpenPosition;
        }
        Debug.Log("Drawer opened!");
    }
}
