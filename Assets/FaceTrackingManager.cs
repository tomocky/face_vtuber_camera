using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using VRM;

public class FaceTrackingManager : MonoBehaviour
{
    [SerializeField] private ARFaceManager faceManager;
    [SerializeField] private GameObject avatarPrefab;
    [SerializeField] private Slider xPoint;
    [SerializeField] private Slider yPoint;
    [SerializeField] private Slider zPoint;
    [SerializeField] private Slider objectScale;
    ARKitFaceSubsystem faceSubsystem;
    GameObject avatar;
    Transform neck;
    Vector3 headSettingParam;
    Vector3 headOffset;
    VRMBlendShapeProxy blendShapeProxy;

    private void Start()
    {
        faceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
        avatar = Instantiate(avatarPrefab);

        // 初期状態で後ろを向いてるため180度回転
        //avatar.transform.Rotate(new Vector3(0f, 180f, 0));
        avatar.transform.Rotate(new Vector3(0f, 0f, 0));

        // 必要な首の関節のTransformを取得
        var animator = avatar.GetComponent<Animator>();
        neck = animator.GetBoneTransform(HumanBodyBones.Neck);

        // アバターの原点座標は足元のため、顔の高さを一致させるためにオフセットを取得
        var head = animator.GetBoneTransform(HumanBodyBones.Head);
        headOffset = new Vector3(head.position.x, head.position.y, head.position.z);

        // VRMの表情を変化させるためのVRMBlendShapeProxyを取得
        blendShapeProxy = avatar.GetComponent<VRMBlendShapeProxy>();
    }

    private void OnEnable()
    {
        faceManager.facesChanged += OnFaceChanged;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFaceChanged;
    }

    private void OnFaceChanged(ARFacesChangedEventArgs eventArgs)
    {
        if (eventArgs.updated.Count != 0)
        {
            var arFace = eventArgs.updated[0];
            if (arFace.trackingState == TrackingState.Tracking
                && (ARSession.state > ARSessionState.Ready))
            {
                UpdateAvatarPositionHeadOffset();
                UpdateAvatarPosition(arFace);
                UpdateBlendShape(arFace);
            }
        }
    }

    private void UpdateAvatarPosition(ARFace arFace)
    {
        // アバターの位置と顔の向きを更新
        avatar.transform.position = arFace.transform.position - headOffset - headSettingParam;
        neck.localRotation = arFace.transform.rotation;
    }

    private void UpdateBlendShape(ARFace arFace)
    {
        // 瞬きと口の開き具合を取得して、アバターに反映
        var blendShapesVRM = new Dictionary<BlendShapeKey, float>();
        using var blendShapesARKit = faceSubsystem.GetBlendShapeCoefficients(arFace.trackableId, Allocator.Temp);

        foreach (var featureCoefficient in blendShapesARKit)
        {
            if (featureCoefficient.blendShapeLocation == ARKitBlendShapeLocation.EyeBlinkLeft)
            {
                blendShapesVRM.Add(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_L), featureCoefficient.coefficient);
            }
            if (featureCoefficient.blendShapeLocation == ARKitBlendShapeLocation.EyeBlinkRight)
            {
                blendShapesVRM.Add(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_R), featureCoefficient.coefficient);
            }
            if (featureCoefficient.blendShapeLocation == ARKitBlendShapeLocation.JawOpen)
            {
                blendShapesVRM.Add(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), featureCoefficient.coefficient);
            }
        }
        blendShapeProxy.SetValues(blendShapesVRM);
    }

    public void UpdateAvatarPositionHeadOffset()
    {
        // アバターの位置と顔の向きを更新
        headSettingParam = new Vector3(xPoint.value, yPoint.value, 0);
        Vector3 lScale = avatarPrefab.transform.localScale;
        avatar.transform.localScale = new Vector3(lScale.x * objectScale.value, lScale.y * objectScale.value, lScale.z * objectScale.value);
    }

    public void UpdateAvatarPositionDep()
    {
        // アバターの位置と顔の向きを更新
        Vector3 depth = new Vector3(0, 0, zPoint.value);
        avatar.transform.position = avatar.transform.position - depth;
    }
}