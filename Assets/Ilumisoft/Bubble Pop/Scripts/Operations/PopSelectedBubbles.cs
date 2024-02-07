namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;

    public class PopSelectedBubbles : ICoroutine
    {
        Selection selection;
        SFXPlayer sfxPlayer;

        public PopSelectedBubbles(Selection selection, SFXPlayer sfxPlayer)
        {
            this.sfxPlayer = sfxPlayer;
            this.selection = selection;
        }

        public IEnumerator Execute()
        {
            sfxPlayer.ChangePitch(selection.Selected.Count - 2);
            //sfxPlayer.PlayPopSFX();
            sfxPlayer.PlaySelectSFX();
            Score.Add(new ScoreRevenue(selection).Value);
            if (selection.Selected.Count > 0)
                TextureChanger.Instance.ChangeTexture(selection.Selected[0].spriteRenderer.sprite.texture);
            foreach (var bubble in selection.Selected)
            {
                bubble.Pop();
            }

            selection.Clear();

            yield return new WaitForSeconds(0.25f);
        }
    }
}