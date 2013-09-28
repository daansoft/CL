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

        public Concept MergeConceptWithDeltas(Concept concept, List<Delta> deltas)
        {
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
                        break;
                    case DeltaAction.AddRelatedConcept:
                        if (concept != null)
                        {
                            var val = (string)delta.Value;
                            if (!concept.RelatedConceptIds.Contains(val))
                            {
                                concept.RelatedConceptIds.Add(val);
                                if (concept.Action == CommitAction.NoChange)
                                {
                                    concept.Action = CommitAction.Update;
                                }
                            }
                        }
                        break;
                    case DeltaAction.AddConceptUser:
                        if (concept != null)
                        {
                            var val = (Guid)delta.Value;
                            if (!concept.UserIds.Contains(val))
                            {
                                concept.UserIds.Add(val);
                                if (concept.Action == CommitAction.NoChange)
                                {
                                    concept.Action = CommitAction.Update;
                                }
                            }
                        }
                        break;
                    case DeltaAction.UpdateConceptDescription:
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            if (concept.Action == CommitAction.NoChange)
                            {
                                concept.Action = CommitAction.Update;
                            }
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
                            if (concept.Action == CommitAction.NoChange)
                            {
                                concept.Action = CommitAction.Update;
                            }
                        }
                        break;
                    case DeltaAction.RemoveConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Remove((Guid)delta.Value);
                            if (concept.Action == CommitAction.NoChange)
                            {
                                concept.Action = CommitAction.Update;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return concept;
        }

        public List<Concept> MergeConceptWithDeltas(List<Concept> concepts, List<Delta> deltas)
        {
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
                        var conceptId = (string)delta.Value;
                        if (!result.Any(r => r.Id == conceptId))
                        {
                            result.Add(new Concept()
                            {
                                Id = conceptId,
                                Action = CommitAction.Create
                            });
                        }
                        else
                        {
                            throw new Exception("AddConcept");
                        }
                        break;
                    case DeltaAction.AddRelatedConcept:
                        if (concept != null)
                        {
                            var val = (string)delta.Value;
                            if (!concept.RelatedConceptIds.Contains(val))
                            {
                                concept.RelatedConceptIds.Add(val);
                                if (concept.Action == CommitAction.NoChange)
                                {
                                    concept.Action = CommitAction.Update;
                                }
                            }
                            else
                            {
                                throw new Exception("AddRelatedConcept");
                            }
                        }
                        break;
                    case DeltaAction.AddConceptUser:
                        if (concept != null)
                        {
                            var val = (Guid)delta.Value;
                            if (!concept.UserIds.Contains(val))
                            {
                                concept.UserIds.Add(val);
                                if (concept.Action == CommitAction.NoChange)
                                {
                                    concept.Action = CommitAction.Update;
                                }
                            }
                            else
                            {
                                throw new Exception("AddConceptUser");
                            }
                        }
                        break;
                    case DeltaAction.UpdateConceptDescription:
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            if (concept.Action == CommitAction.NoChange)
                            {
                                concept.Action = CommitAction.Update;
                            }
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
                            if (concept.Action == CommitAction.NoChange)
                            {
                                concept.Action = CommitAction.Update;
                            }
                        }
                        break;
                    case DeltaAction.RemoveConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Remove((Guid)delta.Value);
                            if (concept.Action == CommitAction.NoChange)
                            {
                                concept.Action = CommitAction.Update;
                            }
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

        public List<string> FindRelatedConceptsIdsByConceptId(string conceptId, List<Delta> deltas)
        {
            var relatedConcept = deltas.Where(r => r.ObjectId == conceptId && r.Action == DeltaAction.AddRelatedConcept);

            return relatedConcept.Select(r => (string)r.Value).ToList();
        }

        public Concept ReadConceptByConceptId(string conceptId, List<Delta> deltas)
        {
            var delta = deltas.SingleOrDefault(r => r.ObjectId == conceptId && r.Action == DeltaAction.AddConcept);

            if (delta == null)
            {
                return null;
            }

            return (Concept)delta.Value;
        }
    }
}
