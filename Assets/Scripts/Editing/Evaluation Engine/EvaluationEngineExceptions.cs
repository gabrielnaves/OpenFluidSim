using System;
using UnityEngine;

namespace EvaluationExceptions {

    public abstract class EvaluationException : Exception {

        public abstract string[] GetProblemList();
    }

    public class OpenConnectorException : EvaluationException {
        public BaseComponent[] components;

        public OpenConnectorException(BaseComponent[] components) {
            this.components = components;
        }

        public override string Message {
            get {
                if (components.Length == 1)
                    return "Componente não conectado:";
                else return "Componentes não conectados:";
            }
        }

        public override string[] GetProblemList() {
            string[] result = new string[components.Length];
            for (int i = 0; i < components.Length; ++i)
                result[i] = components[i].gameObject.name;
            return result;
        }
    }

    public class UnassignedContactException : EvaluationException {
        public Contact[] contacts;

        public UnassignedContactException(Contact[] contacts) {
            this.contacts = contacts;
        }

        public override string Message {
            get {
                if (contacts.Length == 1)
                    return "Contato não configurado:";
                else return "Contatos não configurados:";
            }
        }

        public override string[] GetProblemList() {
            string[] result = new string[contacts.Length];
            for (int i = 0; i < contacts.Length; ++i)
                result[i] = contacts[i].gameObject.name;
            return result;
        }
    }
}
