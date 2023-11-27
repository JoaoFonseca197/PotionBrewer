using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GameController _gameController;

    private void Awake()
    {
        _gameController = GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))        _gameController.AddColorCyan();
        if (Input.GetKeyDown(KeyCode.M))        _gameController.AddColorMagenta();
        if(Input.GetKeyDown(KeyCode.Y))         _gameController.AddColorYellow();
        if (Input.GetKeyDown(KeyCode.S))        _gameController.Shake();
        if (Input.GetKeyDown(KeyCode.W))        _gameController.Wait();
        if (Input.GetKeyDown(KeyCode.D))        _gameController.Done();
        if (Input.GetKeyDown(KeyCode.B))        _gameController.ClearLeds();
        if (Input.GetKeyDown(KeyCode.Space))    _gameController.CheckForWaitTime();
    }
}
