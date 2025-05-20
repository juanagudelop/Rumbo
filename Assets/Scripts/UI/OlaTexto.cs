using UnityEngine;
using TMPro;
using System.Collections;

public class OlaDeTexto : MonoBehaviour
{
    public float alturaSalto = 10f;      // Qué tanto sube cada letra
    public float duracionSalto = 0.3f;   // Duración del salto por letra
    public float retrasoEntreLetras = 0.08f; // Retraso entre letras
    public float tiempoEntreOlas = 1f;   // Tiempo entre cada ciclo de ola

    private TMP_Text textoTMP;
    private Vector3[] vertices;
    private TMP_TextInfo textInfo;

    void Start()
    {
        textoTMP = GetComponent<TMP_Text>();
        textoTMP.ForceMeshUpdate();
        StartCoroutine(AnimarOla());
    }

    IEnumerator AnimarOla()
    {
        while (true)
        {
            textoTMP.ForceMeshUpdate();
            textInfo = textoTMP.textInfo;

            int letraTotal = textInfo.characterCount;

            for (int i = 0; i < letraTotal; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                StartCoroutine(AnimarLetra(i));
                yield return new WaitForSeconds(retrasoEntreLetras);
            }

            yield return new WaitForSeconds(tiempoEntreOlas);
        }
    }

    IEnumerator AnimarLetra(int index)
    {
        float timer = 0f;
        TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
        int materialIndex = charInfo.materialReferenceIndex;
        int vertexIndex = charInfo.vertexIndex;
        Vector3[] verts = textInfo.meshInfo[materialIndex].vertices;

        Vector3 offset = new Vector3(0, alturaSalto, 0);

        while (timer < duracionSalto)
        {
            float t = timer / duracionSalto;
            float curve = Mathf.Sin(t * Mathf.PI); // Suave ida y vuelta
            Vector3 animOffset = offset * curve;

            verts[vertexIndex + 0] = textInfo.characterInfo[index].bottomLeft + animOffset;
            verts[vertexIndex + 1] = textInfo.characterInfo[index].topLeft + animOffset;
            verts[vertexIndex + 2] = textInfo.characterInfo[index].topRight + animOffset;
            verts[vertexIndex + 3] = textInfo.characterInfo[index].bottomRight + animOffset;

            textoTMP.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

            timer += Time.deltaTime;
            yield return null;
        }

        // Restaurar posición original al final (por si se desajusta)
        textoTMP.ForceMeshUpdate();
    }
}
