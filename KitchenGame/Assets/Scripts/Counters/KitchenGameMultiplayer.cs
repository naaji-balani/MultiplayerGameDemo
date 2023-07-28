using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenGameMultiplayer : NetworkBehaviour
{

    [SerializeField] private KitchenObjectListSo kitchenObjectListSO;

    public static KitchenGameMultiplayer Instance {get;private set;}

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        SpawnKitchenObjectServerRpc(GetKitchenObjectSOIndex(kitchenObjectSO), kitchenObjectParent.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnKitchenObjectServerRpc(int kitchenObjectSOIndex, NetworkObjectReference kitchenObjectParentNetworkObejctReference)
    {
        KitchenObjectSO kitchenobjectSO = GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

        Transform kitchenObjectTransform = Instantiate(kitchenobjectSO.prefab);

        NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
        kitchenObjectNetworkObject.Spawn(true);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObjectParentNetworkObejctReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
        IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

    }

    private int GetKitchenObjectSOIndex(KitchenObjectSO kitchenobjectSO)
    {
        return kitchenObjectListSO.kitchenObkectListSoList.IndexOf(kitchenobjectSO);
    }

    private KitchenObjectSO GetKitchenObjectSOFromIndex(int kitchenobjectSoIndex)
    {
        return kitchenObjectListSO.kitchenObkectListSoList[kitchenobjectSoIndex];
    }
    

}
