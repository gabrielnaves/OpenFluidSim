using System;
using UnityEngine;

namespace EvaluationExceptions {

    public class OpenConnectorException : Exception {
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
    }

    public class UnassignedContactException : Exception {
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
    }
}
