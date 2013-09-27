using DaAn.ConceptLog.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Repositories
{
    public class DeltaRepository
    {
        private static List<Delta> Deltas;

        static DeltaRepository()
        {
            DeltaRepository.Deltas = new List<Delta>();
        }

        public Concept MergeConceptWithDelta(Concept concept)
        {
            var deltas = this.FindAll();

            if (deltas == null)
            {
                return concept;
            }

            var deltasForConcept = deltas.Where(r => r.ObjectId == concept.Id).ToList();

            foreach (var delta in deltasForConcept)
            {
                switch (delta.Action)
                {
                    case DeltaAction.AddConcept:
                        /*var newConcept = (Concept)delta.Value;
                        if (newConcept != null)
                        {
                            result.Add(newConcept);
                            newConcept.Action = CommitAction.Create;
                        }*/
                        break;
                    case DeltaAction.AddRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Add((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.AddConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Add((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.UpdateConceptDescription:
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConcept:
                        if (concept != null)
                        {
                            //result.Remove(concept);

                            concept = null;
                        }
                        break;
                    case DeltaAction.RemoveRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Remove((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Remove((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    default:
                        break;
                }
            }

            return concept;
        }

        public List<Concept> MergeConceptWithDeltas(List<Concept> concepts)
        {
            var deltas = this.FindAll();

            var result = concepts.ToList();

            if (deltas == null)
            {
                return result;
            }

            foreach (var delta in deltas.OrderBy(r => (int)r.Action))
            {
                var concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);

                switch (delta.Action)
                {
                    case DeltaAction.AddConcept:
                        var newConcept = (Concept)delta.Value;
                        if (newConcept != null)
                        {
                            result.Add(newConcept);
                            newConcept.Action = CommitAction.Create;
                        }
                        break;
                    case DeltaAction.AddRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Add((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.AddConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Add((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.UpdateConceptDescription:
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConcept:
                        if (concept != null)
                        {
                            result.Remove(concept);
                        }
                        break;
                    case DeltaAction.RemoveRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Remove((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Remove((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public void DeleteAll()
        {
            DeltaRepository.Deltas.Clear();
        }

        public List<Delta> FindAll()
        {
            return DeltaRepository.Deltas;
        }

        public void Create(Delta delta)
        {
            DeltaRepository.Deltas.Add(delta);
        }

        public void Create(List<Delta> deltas)
        {
            DeltaRepository.Deltas.AddRange(deltas);
        }
    }
}
