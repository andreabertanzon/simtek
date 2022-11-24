<script lang="ts">
	import { rootStore } from "../stores/store";
	import { addToast } from "../stores/toasts";
	import { fly, fade } from "svelte/transition";
	import type { Material, ChosenMaterial } from "../models/material";
	import {
		WorkerInterventionSchema,
		type WorkerIntervention,
	} from "../models/Worker";
	import type { CurrentIntervention } from "src/models/intervention";

	// let workers = $rootStore.workers;

	let descr = "";
	let materialInput = "";

	//*** INTERVENTION: ***
	let workers = $rootStore.workers.map((item) => {
		try {
			let worker: WorkerIntervention = WorkerInterventionSchema.parse(item);
			return worker;
		} catch (err) {
			console.log(err);
			return WorkerInterventionSchema.parse({
				Name: "Error",
				SpentHours: 0,
				PriceHour: 0,
				Checked: false,
			});
		}
	});

	let title = "";
	let descriptions: string[] = [];
	let materials: Material[] = [];
	let chosenMaterials: ChosenMaterial[] = [];

	// *****************

	function filterMaterials() {
		if (materialInput.trim() === "" || materialInput.length < 3) {
			materials = [];
			return;
		}
		let matArray = materialInput.split(" ");
		if (matArray.length <= 1) {
			materials = $rootStore.materials.filter((item) =>
				item.Name.toLowerCase().includes(materialInput.trim().toLowerCase())
			);
			return;
		}

		materials = $rootStore.materials
			.filter((item) =>
				item.Name.toLowerCase().match(matArray[0].trim().toLowerCase())
			)
			.filter((item) =>
				item.Name.toLowerCase().match(matArray[1].trim().toLowerCase())
			);
	}

	$: materialInput && filterMaterials();

	$: getPriceTotal = () => {
		if (workers.filter((item) => item.Checked).length === 0) return 0;
		let result = workers
			.filter((item) => item.Checked)
			.map(
				(item) =>
					item.PricePerHour *
					(item.WorkedHours === undefined ? 0 : item.WorkedHours)
			)
			.reduce((sum, a) => sum + a);

		return result === undefined ? 0 : result;
	};

	$: getHourTotal = () => {
		if (workers.filter((item) => item.Checked).length === 0) return 0;
		let result = workers
			.filter((item) => item.Checked)
			.map((item) => (item.WorkedHours === undefined ? 0 : item.WorkedHours))
			.reduce((sum, a) => sum + a);
		return result === undefined ? 0 : result;
	};

	function addDescription(e: KeyboardEvent | null) {
		if (e === null || e.key === "Enter") {
			if (descr !== "") {
				descriptions = [...descriptions, descr];
			}
			descr = "";
		}
	}
	function removeDescr(i: number) {
		descriptions = descriptions.filter((_, idx) => idx !== i);
	}
	function removeMaterial(i: number) {
		chosenMaterials = chosenMaterials.filter((_, idx) => idx !== i);
	}

	function addMaterial(m: Material) {
		try {
			let result: ChosenMaterial = {
				Company: m.Company,
				Name: m.Name,
				Pieces: 0,
				Price: m.Price,
				Unit: m.Unit,
				Checked: true,
			};
			chosenMaterials = [...chosenMaterials, result];
			materialInput = "";
			materials = [];
		} catch (err) {
			console.log(err);
		}
	}

	function saveIntervention() {
		let currentIntervention: CurrentIntervention = {
			date: new Date().toLocaleDateString("it-IT").toString(),
			spentHours: getHourTotal(),
			title: title,
			descriptions: descriptions,
			workers: workers,
			completed: false,
			clientName: "",
			workCost: getHourTotal(),
			materialCost: 0,
			numberOfWorkers: 0,
			notes: [],
			materials: chosenMaterials,
		};
		rootStore.addIntervention(currentIntervention);

		addToast({
			id: 1,
			message: "Saved",
			type: "success",
			dismissable: true,
			timeout: 1000,
		});
	}
	let hideWorkers = false;
</script>

<div
	class="grid grid-cols-1 h-screen gap-2 md:grid-cols-3"
	in:fade={{ duration: 500, delay: 600 }}
	out:fly={{ x: -200, duration: 500 }}
>
	<div class="col-span-2 overflow-y-scroll">
		<div class="m-4 grid grid-flow-row scroll-auto">
			<h1>Definisci Intervento</h1>
			<div class={hideWorkers ? `hidden` : ``}>
				<div
					class="mt-8 grid grid-flow-row rounded-md border {workers.length === 0
						? 'hidden'
						: ''}"
					transition:fly={{ x: -200, duration: 500 }}
				>
					<div
						class="overflow-y-auto scroll-auto max-h-72 {hideWorkers
							? `hidden`
							: ``}"
					>
						{#each workers as worker}
							<!-- svelte-ignore a11y-click-events-have-key-events -->
							<div
								on:click={() => ""}
								class="p-2 border cursor-pointer transion-colors 
 					 flex flex-row justify-start"
							>
								<div class="flex self-center h-6 w-6 flex-1 mr-4">
									<input
										id="checked-checkbox"
										type="checkbox"
										bind:checked={worker.Checked}
										class="ml-1 mr-6 flex-auto"
									/>
									<p class="text-sm font-normal self-center">
										{worker.Name}
									</p>
								</div>
								<div class="flex flex-col flex-auto">
									<label for="hours" class="text-xs font-thin">ore spese:</label
									>
									<input
										bind:value={worker.WorkedHours}
										id="hours"
										type="number"
										class="text-sm font-normal border rounded-md px-1 py-0.5 w-12"
									/>
								</div>
								<div class="flex flex-col flex-auto">
									<label for="hours" class="text-xs font-thin">prezzo:</label>
									<input
										bind:value={worker.PricePerHour}
										id="euro"
										type="number"
										class="text-sm font-normal border rounded-md px-1 py-0.5 w-14"
									/>
								</div>
							</div>
						{/each}
					</div>
				</div>

				<div class="mt-6 flex flex-col">
					<label for="name" class="text-sm font-thin">Breve titolo:</label>
					<div class="flex">
						<input
							bind:value={title}
							id="name"
							type="text"
							class="border border-slate-600 rounded-lg font-normal p-2 text-sm flex-1"
						/>
					</div>
				</div>
			</div>
			<div class="mt-6 flex flex-col {!hideWorkers ? `hidden` : ``}">
				<div
					class="max-h-28 {descriptions.length > 0
						? `border border-slate-100 rounded-md p-1 scroll-m-0 mb-1`
						: ``} scroll-auto overflow-y-scroll"
				>
					{#each descriptions as des, i}
						<div class="flex flex-row items-center mb-2">
							<!-- svelte-ignore a11y-click-events-have-key-events -->
							<svg
								on:click={() => removeDescr(i)}
								xmlns="http://www.w3.org/2000/svg"
								viewBox="0 0 24 24"
								fill="currentColor"
								class="w-4 h-4 shrink-0"
							>
								<path
									fill-rule="evenodd"
									d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25zm-1.72 6.97a.75.75 0 10-1.06 1.06L10.94 12l-1.72 1.72a.75.75 0 101.06 1.06L12 13.06l1.72 1.72a.75.75 0 101.06-1.06L13.06 12l1.72-1.72a.75.75 0 10-1.06-1.06L12 10.94l-1.72-1.72z"
									clip-rule="evenodd"
								/>
							</svg>

							<p class="ml-1 text-black text-sm font-thin break-words">
								{des}
							</p>
						</div>
					{/each}
				</div>
				<label for="surname" class="text-sm font-thin">Descrizione:</label>
				<div class="flex">
					<input
						bind:value={descr}
						on:keypress={addDescription}
						id="surname"
						type="text"
						class="flex-1 border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
					<!-- svelte-ignore a11y-click-events-have-key-events -->
					<svg
						on:click={() => addDescription(null)}
						xmlns="http://www.w3.org/2000/svg"
						fill="none"
						viewBox="0 0 24 24"
						stroke-width="1.5"
						stroke="currentColor"
						class="w-8 h-8 ml-2 self-center"
					>
						<path
							stroke-linecap="round"
							stroke-linejoin="round"
							d="M12 9v6m3-3H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z"
						/>
					</svg>
				</div>
			</div>
		</div>
		<div
			class={hideWorkers ? `` : `hidden`}
			transition:fade={{ duration: 500 }}
		>
			<label for="materials" class="mx-4 text-sm font-thin">Materiali:</label>

			<div
				class="flex flex-col mx-4 max-h-28 {descriptions.length > 0
					? `border border-slate-100 rounded-md p-1 scroll-m-0 mb-1`
					: ``} scroll-auto overflow-y-scroll"
			>
				{#each chosenMaterials as chMat, i}
					<div class="flex flex-row items-center mb-2 justify-start">
						<!-- svelte-ignore a11y-click-events-have-key-events -->
						<svg
							on:click={() => removeMaterial(i)}
							xmlns="http://www.w3.org/2000/svg"
							viewBox="0 0 24 24"
							fill="currentColor"
							class="w-4 h-4 shrink-0"
						>
							<path
								fill-rule="evenodd"
								d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25zm-1.72 6.97a.75.75 0 10-1.06 1.06L10.94 12l-1.72 1.72a.75.75 0 101.06 1.06L12 13.06l1.72 1.72a.75.75 0 101.06-1.06L13.06 12l1.72-1.72a.75.75 0 10-1.06-1.06L12 10.94l-1.72-1.72z"
								clip-rule="evenodd"
							/>
						</svg>
						<div class="flex flex-row mr-4">
							<p class="ml-1 text-black text-sm font-thin break-words">
								{chMat.Name},
							</p>
						</div>
						<label for="pieces" class="font-thin text-sm mr-2"
							>{chMat.Unit}:</label
						>
						<input
							bind:value={chMat.Pieces}
							id="pieces"
							type="number"
							class="text-sm border font-thin border-slate-100 rounded-md p-0.5 w-10 mr-2"
						/>
						<label for="matPrice" class="font-thin text-sm mr-2">Price:</label>
						<input
							bind:value={chMat.Price}
							id="matPrice"
							type="number"
							class="text-sm font-thin border border-slate-100 rounded-md p-0.5 w-10"
						/>
					</div>
				{/each}
			</div>
			<div class="mx-4 grid grid-flow-row rounded-md border">
				<input
					bind:value={materialInput}
					id="materials"
					type="text"
					class="pl-2 py-1 {materials.length == 0
						? `rounded-md`
						: `rounded-t-md`} border border-slate-600 text-sm font-normal"
				/>
				<div class="overflow-y-auto scroll-auto max-h-96">
					{#each materials as material}
						<!-- svelte-ignore a11y-click-events-have-key-events -->
						<div
							class="p-4 border cursor-pointer transion-colors hover:bg-[#afb39a]
 				 flex justify-between"
							on:click={() => addMaterial(material)}
						>
							<p class="text-sm font-normal">{material.Name}</p>
						</div>
					{/each}
				</div>
			</div>
			<div class="flex flex-col">
				<label for="notes" class="text-sm font-thin mx-4 mt-4">Note: </label>
				<textarea
					id="notes"
					type="text"
					rows="5"
					class="flex-1 border border-slate-600 rounded-md mx-4 my-4 text-sm font-normal p-2"
				/>
			</div>
		</div>
		<div class="m-4">
			{#if !hideWorkers}
				<button
					on:click={() => (hideWorkers = true)}
					class="p-2 px-4 rounded-full border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
					>Prosegui</button
				>
			{/if}
			{#if hideWorkers}
				<button
					on:click={saveIntervention}
					class="p-2 px-4 rounded-full border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
					>Salva</button
				>
				<button
					on:click={() => (hideWorkers = false)}
					class="p-2 px-4 rounded-full border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
					>Indietro</button
				>
			{/if}
		</div>
	</div>
	<div
		class="h-screen overflow-y-scroll bg-slate-100 p-2 hidden md:flex md:flex-col"
	>
		<h2 class="text-sm">Lavoratori:</h2>
		<div class="flex my-4">
			<table class="table-fixed flex-1">
				<tbody>
					{#each workers.filter((item) => item.Checked) as worker}
						<tr class="border border-slate-900">
							<td><p class="font-normal text-sm">{worker.Name}</p></td>
							<td><p class="font-normal text-sm">{worker.WorkedHours}h</p></td>
							<td
								><p class="font-normal text-sm">
									{worker.PricePerHour *
										(worker.WorkedHours === undefined
											? 0
											: worker.WorkedHours)}€
								</p></td
							>
						</tr>
					{/each}
					<tr class="border border-slate-900">
						<td><p class="font-bold text-sm">Totale</p></td>
						<td><p class="font-bold text-sm">{getHourTotal()}h</p></td>
						<td><p class="font-bold text-sm">{getPriceTotal()}€</p></td>
					</tr>
				</tbody>
			</table>
		</div>
		<h2 class="text-sm">Descrizione:</h2>
		<div class="flex">
			<table class="flex-1">
				<tbody>
					{#each descriptions as des}
						<tr class="break-normal">
							<td><p class="my-1 break-normal font-normal text-sm">{des}</p></td
							>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
		<h2 class="text-sm mt-2">Materiali:</h2>
		<div class="flex">
			<table class="table-fixed  flex-1">
				<tbody>
					{#each chosenMaterials as mat}
						<tr class="border border-slate-900">
							<td><p class="font-normal text-sm">{mat.Name}</p></td>
							<td
								><p class="font-normal text-sm">
									{mat.Price * mat.Pieces}€
								</p></td
							>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>
	<div />
</div>
