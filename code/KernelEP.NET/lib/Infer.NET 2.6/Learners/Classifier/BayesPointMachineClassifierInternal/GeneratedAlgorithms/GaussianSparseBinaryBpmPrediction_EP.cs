// <auto-generated />
#pragma warning disable 1570, 1591

using System;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using MicrosoftResearch.Infer.Collections;
using MicrosoftResearch.Infer.Factors;

namespace MicrosoftResearch.Infer.Models.User
{
	/// <summary>
	/// Generated algorithm for performing inference.
	/// </summary>
	/// <remarks>
	/// If you wish to use this class directly, you must perform the following steps:
	/// 1) Create an instance of the class.
	/// 2) Set the value of any externally-set fields e.g. data, priors.
	/// 3) Call the Execute(numberOfIterations) method.
	/// 4) Use the XXXMarginal() methods to retrieve posterior marginals for different variables.
	/// 
	/// Generated by Infer.NET 2.6.41114.1 at 11:57 PM on Friday, November 14, 2014.
	/// </remarks>
	public partial class GaussianSparseBinaryBpmPrediction_EP : IGeneratedAlgorithm
	{
		#region Fields
		/// <summary>Field backing the NumberOfIterationsDone property</summary>
		private int numberOfIterationsDone;
		/// <summary>Field backing the InstanceCount property</summary>
		private int instanceCount;
		/// <summary>Field backing the FeatureCount property</summary>
		private int featureCount;
		/// <summary>Field backing the InstanceFeatureCounts property</summary>
		private int[] instanceFeatureCounts;
		/// <summary>Field backing the FeatureValues property</summary>
		private double[][] featureValues;
		/// <summary>Field backing the FeatureIndexes property</summary>
		private int[][] featureIndexes;
		/// <summary>Field backing the WeightPriors property</summary>
		private DistributionStructArray<Gaussian,double> weightPriors;
		/// <summary>Field backing the WeightConstraints property</summary>
		private DistributionStructArray<Gaussian,double> weightConstraints;
		/// <summary>The number of iterations last computed by Constant. Set this to zero to force re-execution of Constant</summary>
		public int Constant_iterationsDone;
		/// <summary>The number of iterations last computed by Changed_WeightPriors. Set this to zero to force re-execution of Changed_WeightPriors</summary>
		public int Changed_WeightPriors_iterationsDone;
		/// <summary>The number of iterations last computed by Changed_WeightConstraints_WeightPriors. Set this to zero to force re-execution of Changed_WeightConstraints_WeightPriors</summary>
		public int Changed_WeightConstraints_WeightPriors_iterationsDone;
		/// <summary>The number of iterations last computed by Changed_InstanceCount. Set this to zero to force re-execution of Changed_InstanceCount</summary>
		public int Changed_InstanceCount_iterationsDone;
		/// <summary>The number of iterations last computed by Changed_InstanceCount_InstanceFeatureCounts. Set this to zero to force re-execution of Changed_InstanceCount_InstanceFeatureCounts</summary>
		public int Changed_InstanceCount_InstanceFeatureCounts_iterationsDone;
		/// <summary>The number of iterations last computed by Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts. Set this to zero to force re-execution of Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts</summary>
		public int Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone;
		/// <summary>The number of iterations last computed by Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6. Set this to zero to force re-execution of Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6</summary>
		public int Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone;
		/// <summary>Messages to use of 'Weights'</summary>
		public DistributionStructArray<Gaussian,double>[] Weights_uses_F;
		/// <summary>Messages from use of 'Weights'</summary>
		public DistributionStructArray<Gaussian,double>[] Weights_uses_B;
		/// <summary>Buffer for ReplicateOp_Divide.UsesAverageConditional<DistributionStructArray<Gaussian, double>></summary>
		public DistributionStructArray<Gaussian,double> Weights_uses_F_marginal;
		/// <summary>Buffer for ReplicateOp_Divide.Marginal<DistributionStructArray<Gaussian, double>></summary>
		public DistributionStructArray<Gaussian,double> Weights_uses_B_toDef;
		/// <summary>Buffer for JaggedSubarrayOp<double>.ItemsAverageConditional<DistributionStructArray<Gaussian, double>, Gaussian, DistributionStructArray<Gaussian, double>></summary>
		public DistributionStructArray<Gaussian,double> Weights_uses_F_1__marginal;
		public DistributionStructArray<Bernoulli,bool> Labels_F;
		public DistributionRefArray<DistributionStructArray<Gaussian,double>,double[]> Weights_FeatureIndexes_F;
		/// <summary>Message to marginal of 'Labels'</summary>
		public DistributionStructArray<Bernoulli,bool> Labels_marginal_F;
		/// <summary>Message from use of 'Labels'</summary>
		public DistributionStructArray<Bernoulli,bool> Labels_use_B;
		public DistributionRefArray<DistributionStructArray<Gaussian,double>,double[]> IndexedWeights_B;
		public DistributionRefArray<DistributionStructArray<Gaussian,double>,double[]> FeatureScores_F;
		public DistributionStructArray<Gaussian,double> Score_F;
		public DistributionStructArray<Gaussian,double> NoisyScore_F;
		#endregion

		#region Properties
		/// <summary>The number of iterations done from the initial state</summary>
		public int NumberOfIterationsDone
		{
			get {
				return this.numberOfIterationsDone;
			}
		}

		/// <summary>The externally-specified value of 'InstanceCount'</summary>
		public int InstanceCount
		{
			get {
				return this.instanceCount;
			}
			set {
				if (this.instanceCount!=value) {
					this.instanceCount = value;
					this.numberOfIterationsDone = 0;
					this.Changed_InstanceCount_iterationsDone = 0;
					this.Changed_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
					this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
					this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
				}
			}
		}

		/// <summary>The externally-specified value of 'FeatureCount'</summary>
		public int FeatureCount
		{
			get {
				return this.featureCount;
			}
			set {
				if (this.featureCount!=value) {
					this.featureCount = value;
					this.numberOfIterationsDone = 0;
				}
			}
		}

		/// <summary>The externally-specified value of 'InstanceFeatureCounts'</summary>
		public int[] InstanceFeatureCounts
		{
			get {
				return this.instanceFeatureCounts;
			}
			set {
				if ((value!=null)&&(value.Length!=this.instanceCount)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.instanceCount)+" was expected for variable \'InstanceFeatureCounts\'");
				}
				this.instanceFeatureCounts = value;
				this.numberOfIterationsDone = 0;
				this.Changed_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
				this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
				this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
			}
		}

		/// <summary>The externally-specified value of 'FeatureValues'</summary>
		public double[][] FeatureValues
		{
			get {
				return this.featureValues;
			}
			set {
				if ((value!=null)&&(value.Length!=this.instanceCount)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.instanceCount)+" was expected for variable \'FeatureValues\'");
				}
				this.featureValues = value;
				this.numberOfIterationsDone = 0;
				this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
			}
		}

		/// <summary>The externally-specified value of 'FeatureIndexes'</summary>
		public int[][] FeatureIndexes
		{
			get {
				return this.featureIndexes;
			}
			set {
				if ((value!=null)&&(value.Length!=this.instanceCount)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.instanceCount)+" was expected for variable \'FeatureIndexes\'");
				}
				this.featureIndexes = value;
				this.numberOfIterationsDone = 0;
				this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
				this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
			}
		}

		/// <summary>The externally-specified value of 'WeightPriors'</summary>
		public DistributionStructArray<Gaussian,double> WeightPriors
		{
			get {
				return this.weightPriors;
			}
			set {
				this.weightPriors = value;
				this.numberOfIterationsDone = 0;
				this.Changed_WeightPriors_iterationsDone = 0;
				this.Changed_WeightConstraints_WeightPriors_iterationsDone = 0;
				this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
				this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
			}
		}

		/// <summary>The externally-specified value of 'WeightConstraints'</summary>
		public DistributionStructArray<Gaussian,double> WeightConstraints
		{
			get {
				return this.weightConstraints;
			}
			set {
				this.weightConstraints = value;
				this.numberOfIterationsDone = 0;
				this.Changed_WeightConstraints_WeightPriors_iterationsDone = 0;
				this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
				this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
			}
		}

		#endregion

		#region Methods
		/// <summary>Get the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetObservedValue(string variableName)
		{
			if (variableName=="InstanceCount") {
				return this.InstanceCount;
			}
			if (variableName=="FeatureCount") {
				return this.FeatureCount;
			}
			if (variableName=="InstanceFeatureCounts") {
				return this.InstanceFeatureCounts;
			}
			if (variableName=="FeatureValues") {
				return this.FeatureValues;
			}
			if (variableName=="FeatureIndexes") {
				return this.FeatureIndexes;
			}
			if (variableName=="WeightPriors") {
				return this.WeightPriors;
			}
			if (variableName=="WeightConstraints") {
				return this.WeightConstraints;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Set the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Observed value</param>
		public void SetObservedValue(string variableName, object value)
		{
			if (variableName=="InstanceCount") {
				this.InstanceCount = (int)value;
				return ;
			}
			if (variableName=="FeatureCount") {
				this.FeatureCount = (int)value;
				return ;
			}
			if (variableName=="InstanceFeatureCounts") {
				this.InstanceFeatureCounts = (int[])value;
				return ;
			}
			if (variableName=="FeatureValues") {
				this.FeatureValues = (double[][])value;
				return ;
			}
			if (variableName=="FeatureIndexes") {
				this.FeatureIndexes = (int[][])value;
				return ;
			}
			if (variableName=="WeightPriors") {
				this.WeightPriors = (DistributionStructArray<Gaussian,double>)value;
				return ;
			}
			if (variableName=="WeightConstraints") {
				this.WeightConstraints = (DistributionStructArray<Gaussian,double>)value;
				return ;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Get the marginal distribution (computed up to this point) of a variable</summary>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <returns>The marginal distribution computed up to this point</returns>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public object Marginal(string variableName)
		{
			if (variableName=="Labels") {
				return this.LabelsMarginal();
			}
			throw new ArgumentException("This class was not built to infer "+variableName);
		}

		/// <summary>Get the marginal distribution (computed up to this point) of a variable, converted to type T</summary>
		/// <typeparam name="T">The distribution type.</typeparam>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <returns>The marginal distribution computed up to this point</returns>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public T Marginal<T>(string variableName)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName));
		}

		/// <summary>Get the query-specific marginal distribution of a variable.</summary>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public object Marginal(string variableName, string query)
		{
			if (query=="Marginal") {
				return this.Marginal(variableName);
			}
			throw new ArgumentException(((("This class was not built to infer \'"+variableName)+"\' with query \'")+query)+"\'");
		}

		/// <summary>Get the query-specific marginal distribution of a variable, converted to type T</summary>
		/// <typeparam name="T">The distribution type.</typeparam>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public T Marginal<T>(string variableName, string query)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName, query));
		}

		/// <summary>Update all marginals, by iterating message passing the given number of times</summary>
		/// <param name="numberOfIterations">The number of times to iterate each loop</param>
		/// <param name="initialise">If true, messages that initialise loops are reset when observed values change</param>
		private void Execute(int numberOfIterations, bool initialise)
		{
			this.Constant();
			this.Changed_InstanceCount();
			this.Changed_InstanceCount_InstanceFeatureCounts();
			this.Changed_WeightPriors();
			this.Changed_WeightConstraints_WeightPriors();
			this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts();
			this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6();
			this.numberOfIterationsDone = numberOfIterations;
		}

		/// <summary>Update all marginals, by iterating message-passing the given number of times</summary>
		/// <param name="numberOfIterations">The total number of iterations that should be executed for the current set of observed values.  If this is more than the number already done, only the extra iterations are done.  If this is less than the number already done, message-passing is restarted from the beginning.  Changing the observed values resets the iteration count to 0.</param>
		public void Execute(int numberOfIterations)
		{
			this.Execute(numberOfIterations, true);
		}

		/// <summary>Update all marginals, by iterating message-passing an additional number of times</summary>
		/// <param name="additionalIterations">The number of iterations that should be executed, starting from the current message state.  Messages are not reset, even if observed values have changed.</param>
		public void Update(int additionalIterations)
		{
			this.Execute(this.numberOfIterationsDone+additionalIterations, false);
		}

		private void OnProgressChanged(ProgressChangedEventArgs e)
		{
			// Make a temporary copy of the event to avoid a race condition
			// if the last subscriber unsubscribes immediately after the null check and before the event is raised.
			EventHandler<ProgressChangedEventArgs> handler = this.ProgressChanged;
			if (handler!=null) {
				handler(this, e);
			}
		}

		/// <summary>Reset all messages to their initial values.  Sets NumberOfIterationsDone to 0.</summary>
		public void Reset()
		{
			this.Execute(0);
		}

		/// <summary>Computations that do not depend on observed values</summary>
		private void Constant()
		{
			if (this.Constant_iterationsDone==1) {
				return ;
			}
			// Create array for 'Weights_uses' Forwards messages.
			this.Weights_uses_F = new DistributionStructArray<Gaussian,double>[2];
			// Create array for 'Weights_uses' Backwards messages.
			this.Weights_uses_B = new DistributionStructArray<Gaussian,double>[2];
			this.Constant_iterationsDone = 1;
			this.Changed_WeightPriors_iterationsDone = 0;
			this.Changed_InstanceCount_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of InstanceCount</summary>
		private void Changed_InstanceCount()
		{
			if (this.Changed_InstanceCount_iterationsDone==1) {
				return ;
			}
			// Create array for 'Labels' Forwards messages.
			this.Labels_F = new DistributionStructArray<Bernoulli,bool>(this.instanceCount);
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				this.Labels_F[InstanceRange] = Bernoulli.Uniform();
			}
			// Create array for replicates of 'IndexedWeights_B'
			this.IndexedWeights_B = new DistributionRefArray<DistributionStructArray<Gaussian,double>,double[]>(this.instanceCount);
			// Create array for 'Weights_FeatureIndexes' Forwards messages.
			this.Weights_FeatureIndexes_F = new DistributionRefArray<DistributionStructArray<Gaussian,double>,double[]>(this.instanceCount);
			// Create array for replicates of 'FeatureScores_F'
			this.FeatureScores_F = new DistributionRefArray<DistributionStructArray<Gaussian,double>,double[]>(this.instanceCount);
			// Create array for replicates of 'Score_F'
			this.Score_F = new DistributionStructArray<Gaussian,double>(this.instanceCount);
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				this.Score_F[InstanceRange] = Gaussian.Uniform();
			}
			// Create array for replicates of 'NoisyScore_F'
			this.NoisyScore_F = new DistributionStructArray<Gaussian,double>(this.instanceCount);
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				this.NoisyScore_F[InstanceRange] = Gaussian.Uniform();
			}
			// Create array for 'Labels_marginal' Forwards messages.
			this.Labels_marginal_F = new DistributionStructArray<Bernoulli,bool>(this.instanceCount);
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				this.Labels_marginal_F[InstanceRange] = Bernoulli.Uniform();
			}
			// Create array for 'Labels_use' Backwards messages.
			this.Labels_use_B = new DistributionStructArray<Bernoulli,bool>(this.instanceCount);
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				this.Labels_use_B[InstanceRange] = Bernoulli.Uniform();
			}
			this.Changed_InstanceCount_iterationsDone = 1;
			this.Changed_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
			this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of InstanceCount and InstanceFeatureCounts</summary>
		private void Changed_InstanceCount_InstanceFeatureCounts()
		{
			if (this.Changed_InstanceCount_InstanceFeatureCounts_iterationsDone==1) {
				return ;
			}
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				// Create array for 'IndexedWeights' Backwards messages.
				this.IndexedWeights_B[InstanceRange] = new DistributionStructArray<Gaussian,double>(this.instanceFeatureCounts[InstanceRange]);
				for(int InstanceFeatureRanges = 0; InstanceFeatureRanges<this.instanceFeatureCounts[InstanceRange]; InstanceFeatureRanges++) {
					this.IndexedWeights_B[InstanceRange][InstanceFeatureRanges] = Gaussian.Uniform();
				}
				// Create array for 'Weights_FeatureIndexes' Forwards messages.
				this.Weights_FeatureIndexes_F[InstanceRange] = new DistributionStructArray<Gaussian,double>(this.instanceFeatureCounts[InstanceRange]);
				for(int InstanceFeatureRanges = 0; InstanceFeatureRanges<this.instanceFeatureCounts[InstanceRange]; InstanceFeatureRanges++) {
					this.Weights_FeatureIndexes_F[InstanceRange][InstanceFeatureRanges] = Gaussian.Uniform();
				}
				// Create array for 'FeatureScores' Forwards messages.
				this.FeatureScores_F[InstanceRange] = new DistributionStructArray<Gaussian,double>(this.instanceFeatureCounts[InstanceRange]);
				for(int InstanceFeatureRanges = 0; InstanceFeatureRanges<this.instanceFeatureCounts[InstanceRange]; InstanceFeatureRanges++) {
					this.FeatureScores_F[InstanceRange][InstanceFeatureRanges] = Gaussian.Uniform();
				}
			}
			this.Changed_InstanceCount_InstanceFeatureCounts_iterationsDone = 1;
			this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
			this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of WeightPriors</summary>
		private void Changed_WeightPriors()
		{
			if (this.Changed_WeightPriors_iterationsDone==1) {
				return ;
			}
			for(int _ind = 0; _ind<2; _ind++) {
				this.Weights_uses_B[_ind] = ArrayHelper.MakeUniform<DistributionStructArray<Gaussian,double>>(this.weightPriors);
				this.Weights_uses_F[_ind] = ArrayHelper.MakeUniform<DistributionStructArray<Gaussian,double>>(this.weightPriors);
			}
			// Message to 'Weights_uses' from Replicate factor
			this.Weights_uses_F_marginal = ReplicateOp_Divide.MarginalInit<DistributionStructArray<Gaussian,double>>(this.weightPriors);
			// Message to 'Weights_uses' from Replicate factor
			this.Weights_uses_B_toDef = ReplicateOp_Divide.ToDefInit<DistributionStructArray<Gaussian,double>>(this.weightPriors);
			// Message to 'Weights_FeatureIndexes' from JaggedSubarray factor
			this.Weights_uses_F_1__marginal = JaggedSubarrayOp<double>.MarginalInit<DistributionStructArray<Gaussian,double>>(this.Weights_uses_F[1]);
			this.Changed_WeightPriors_iterationsDone = 1;
			this.Changed_WeightConstraints_WeightPriors_iterationsDone = 0;
			this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of WeightConstraints and WeightPriors</summary>
		private void Changed_WeightConstraints_WeightPriors()
		{
			if (this.Changed_WeightConstraints_WeightPriors_iterationsDone==1) {
				return ;
			}
			// Message to 'Weights_uses' from EqualRandom factor
			this.Weights_uses_B[0] = ArrayHelper.SetTo<DistributionStructArray<Gaussian,double>>(this.Weights_uses_B[0], this.weightConstraints);
			// Message to 'Weights_uses' from Replicate factor
			this.Weights_uses_B_toDef = ReplicateOp_Divide.ToDef<DistributionStructArray<Gaussian,double>>(this.Weights_uses_B, this.Weights_uses_B_toDef);
			// Message to 'Weights_uses' from Replicate factor
			this.Weights_uses_F_marginal = ReplicateOp_Divide.Marginal<DistributionStructArray<Gaussian,double>>(this.Weights_uses_B_toDef, this.weightPriors, this.Weights_uses_F_marginal);
			// Message to 'Weights_uses' from Replicate factor
			this.Weights_uses_F[1] = ReplicateOp_Divide.UsesAverageConditional<DistributionStructArray<Gaussian,double>>(this.Weights_uses_B[1], this.Weights_uses_F_marginal, 1, this.Weights_uses_F[1]);
			this.Changed_WeightConstraints_WeightPriors_iterationsDone = 1;
			this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of FeatureIndexes and WeightPriors and WeightConstraints and InstanceCount and InstanceFeatureCounts</summary>
		private void Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts()
		{
			if (this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone==1) {
				return ;
			}
			// Message to 'Weights_FeatureIndexes' from JaggedSubarray factor
			this.Weights_uses_F_1__marginal = JaggedSubarrayOp<double>.Marginal<DistributionStructArray<Gaussian,double>,Gaussian,object,DistributionStructArray<Gaussian,double>>(this.Weights_uses_F[1], this.IndexedWeights_B, this.featureIndexes, this.Weights_uses_F_1__marginal);
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				// Message to 'Weights_FeatureIndexes' from JaggedSubarray factor
				this.Weights_FeatureIndexes_F[InstanceRange] = JaggedSubarrayOp<double>.ItemsAverageConditional<DistributionStructArray<Gaussian,double>,Gaussian,DistributionStructArray<Gaussian,double>>(this.IndexedWeights_B[InstanceRange], this.Weights_uses_F[1], this.Weights_uses_F_1__marginal, this.featureIndexes, InstanceRange, this.Weights_FeatureIndexes_F[InstanceRange]);
			}
			this.Changed_FeatureIndexes_WeightPriors_WeightConstraints_InstanceCount_InstanceFeatureCounts_iterationsDone = 1;
			this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of InstanceCount and InstanceFeatureCounts and FeatureValues and FeatureIndexes and WeightPriors and WeightConstraints</summary>
		private void Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6()
		{
			if (this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone==1) {
				return ;
			}
			for(int InstanceRange = 0; InstanceRange<this.instanceCount; InstanceRange++) {
				for(int InstanceFeatureRanges = 0; InstanceFeatureRanges<this.instanceFeatureCounts[InstanceRange]; InstanceFeatureRanges++) {
					// Message to 'FeatureScores' from Product factor
					this.FeatureScores_F[InstanceRange][InstanceFeatureRanges] = GaussianProductOp.ProductAverageConditional(this.featureValues[InstanceRange][InstanceFeatureRanges], this.Weights_FeatureIndexes_F[InstanceRange][InstanceFeatureRanges]);
				}
				// Message to 'Score' from Sum factor
				this.Score_F[InstanceRange] = FastSumOp.SumAverageConditional(this.FeatureScores_F[InstanceRange]);
				// Message to 'NoisyScore' from GaussianFromMeanAndVariance factor
				this.NoisyScore_F[InstanceRange] = GaussianFromMeanAndVarianceOp.SampleAverageConditional(this.Score_F[InstanceRange], 1.0);
				// Message to 'Labels' from IsPositive factor
				this.Labels_F[InstanceRange] = IsPositiveOp.IsPositiveAverageConditional(this.NoisyScore_F[InstanceRange]);
				// Message to 'Labels_marginal' from DerivedVariable factor
				this.Labels_marginal_F[InstanceRange] = DerivedVariableOp.MarginalAverageConditional<Bernoulli>(this.Labels_use_B[InstanceRange], this.Labels_F[InstanceRange], this.Labels_marginal_F[InstanceRange]);
			}
			this.Changed_InstanceCount_InstanceFeatureCounts_FeatureValues_FeatureIndexes_WeightPriors_WeightConstrai6_iterationsDone = 1;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Labels' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionStructArray<Bernoulli,bool> LabelsMarginal()
		{
			return this.Labels_marginal_F;
		}

		#endregion

		#region Events
		/// <summary>Event that is fired when the progress of inference changes, typically at the end of one iteration of the inference algorithm.</summary>
		public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
		#endregion

	}

}