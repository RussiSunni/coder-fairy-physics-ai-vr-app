/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using UnityEngine;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;

namespace Facebook.WitAi.TTS.Samples
{
    public class TTSSpeakerInput : MonoBehaviour
    {
       // [SerializeField] private Text _title;
        [SerializeField] private Text _answer;
        [SerializeField] private TTSSpeaker _speaker;    

        // Either say the current phrase or stop talking/loading
        public void SayPhrase(string answer)
        {
            if (_speaker.IsLoading || _speaker.IsSpeaking)
            {
                _speaker.Stop();
            }
            else
            {
                _speaker.Speak(answer);
            }
        }
    }
}
