using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public PlayerHandler player;
    public UIManager ui = new UIManager();

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Dragable")) return;

        ObjectScript obj = collision.gameObject.GetComponent<ObjectScript>();
        if (obj == null) return;

        // Dragging + object table se uth chuka
        if (player.isDragging && obj.hasLeftTable)
        {
            Debug.Log("Table hit during drag → Stop");

            player.DragEnd();
            obj.OnPlaced(gameObject.tag);
            ui.AddToTable(gameObject.tag, 1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Dragable")) return;

        ObjectScript obj = collision.gameObject.GetComponent<ObjectScript>();
        if (obj == null) return;

        obj.OnLeftTable();
        ui.AddToTable(gameObject.tag, -1);

        Debug.Log("Object left " + gameObject.tag);
    }
}
