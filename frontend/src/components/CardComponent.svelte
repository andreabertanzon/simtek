<script lang="ts">
	import type { Intervention } from "src/models/intervention";
	export let currentIntervention: Intervention;
	let completed = currentIntervention.completed ? "#afb39a" : "#7c2d12";
	let completedIcon = currentIntervention.completed ? "#868a73" : "white";
</script>

<div
	class="grid grid-rows-4 min-w-250 max-w-sm bg-white rounded-lg border border-gray-200 shadow-md dark:bg-gray-800 dark:border-gray-700"
>
	<div
		style="background-color:{completed}"
		class="p-2 rounded-t-lg flex flex-row justify-between content-center align-middle"
	>
		<h5
			class="mb-2 mt-2 text-sm font-bold tracking-tight text-white inline-flex"
		>
			{currentIntervention.clientName}:
			{currentIntervention.date}
		</h5>
		<svg
			xmlns="http://www.w3.org/2000/svg"
			width="24"
			height="24"
			fill={completedIcon}
			class="bi bi-pencil-square"
			viewBox="0 0 16 16"
		>
			<path
				d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"
			/>
			<path
				fill-rule="evenodd"
				d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"
			/>
		</svg>
	</div>
	<div class="p-2 bg-white row-span-2">
		<p style="font-weight:normal;" class="pt-4 text-sm">
			{currentIntervention.title}
		</p>
	</div>
	<div
		class="p-2 bg-white rounded-b-lg grid grid-cols-3 content-center justify-items-center"
	>
		<!-- Icon -->
		<div class="mr-2 flex">
			<svg
				xmlns="http://www.w3.org/2000/svg"
				width="20"
				height="20"
				fill="#fb923c"
				class="bi bi-alarm-fill"
				viewBox="0 0 16 16"
			>
				<path
					d="M6 .5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1H9v1.07a7.001 7.001 0 0 1 3.274 12.474l.601.602a.5.5 0 0 1-.707.708l-.746-.746A6.97 6.97 0 0 1 8 16a6.97 6.97 0 0 1-3.422-.892l-.746.746a.5.5 0 0 1-.707-.708l.602-.602A7.001 7.001 0 0 1 7 2.07V1h-.5A.5.5 0 0 1 6 .5zm2.5 5a.5.5 0 0 0-1 0v3.362l-1.429 2.38a.5.5 0 1 0 .858.515l1.5-2.5A.5.5 0 0 0 8.5 9V5.5zM.86 5.387A2.5 2.5 0 1 1 4.387 1.86 8.035 8.035 0 0 0 .86 5.387zM11.613 1.86a2.5 2.5 0 1 1 3.527 3.527 8.035 8.035 0 0 0-3.527-3.527z"
				/>
			</svg>
			<span class="text-base font-normal leading-tight text-gray-500 ml-2"
				>{currentIntervention.spentHours}h</span
			>
		</div>
		<div class="mr-2 flex">
			<svg
				xmlns="http://www.w3.org/2000/svg"
				width="20"
				height="20"
				fill="#fb923c"
				class="bi bi-credit-card-fill"
				viewBox="0 0 16 16"
			>
				<path
					d="M0 4a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v1H0V4zm0 3v5a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7H0zm3 2h1a1 1 0 0 1 1 1v1a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1v-1a1 1 0 0 1 1-1z"
				/>
			</svg>
			<span class="text-base font-normal leading-tight text-gray-500 ml-2"
				>{currentIntervention.workCost}€</span
			>
		</div>
		<div class="mr-2 flex">
			<svg
				xmlns="http://www.w3.org/2000/svg"
				width="20"
				height="20"
				fill="#fb923c"
				class="bi bi-tools"
				viewBox="0 0 16 16"
			>
				<path
					d="M1 0 0 1l2.2 3.081a1 1 0 0 0 .815.419h.07a1 1 0 0 1 .708.293l2.675 2.675-2.617 2.654A3.003 3.003 0 0 0 0 13a3 3 0 1 0 5.878-.851l2.654-2.617.968.968-.305.914a1 1 0 0 0 .242 1.023l3.356 3.356a1 1 0 0 0 1.414 0l1.586-1.586a1 1 0 0 0 0-1.414l-3.356-3.356a1 1 0 0 0-1.023-.242L10.5 9.5l-.96-.96 2.68-2.643A3.005 3.005 0 0 0 16 3c0-.269-.035-.53-.102-.777l-2.14 2.141L12 4l-.364-1.757L13.777.102a3 3 0 0 0-3.675 3.68L7.462 6.46 4.793 3.793a1 1 0 0 1-.293-.707v-.071a1 1 0 0 0-.419-.814L1 0zm9.646 10.646a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708zM3 11l.471.242.529.026.287.445.445.287.026.529L5 13l-.242.471-.026.529-.445.287-.287.445-.529.026L3 15l-.471-.242L2 14.732l-.287-.445L1.268 14l-.026-.529L1 13l.242-.471.026-.529.445-.287.287-.445.529-.026L3 11z"
				/>
			</svg>
			<span class="text-base font-normal leading-tight text-gray-500 ml-2"
				>{currentIntervention.materialCost}€</span
			>
		</div>
	</div>
</div>
