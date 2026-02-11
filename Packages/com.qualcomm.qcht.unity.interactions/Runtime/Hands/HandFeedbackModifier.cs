// /******************************************************************************
//  * File: HandFeedbackModifier.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  * Confidential and Proprietary - Qualcomm Technologies, Inc.
//  *
//  ******************************************************************************/

using System.Collections.Generic;
using UnityEngine;


namespace QCHT.Interactions.Hands
{
    [RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
    public class HandFeedbackModifier : MonoBehaviour, IHandFeedbackModifier
    {
        [field: SerializeField] public List<HandPartState> Hovered { get; private set; }
        [field: SerializeField] public List<HandPartState> Selected { get; private set; }
    }
}