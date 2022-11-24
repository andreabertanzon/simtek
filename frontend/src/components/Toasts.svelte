<script lang="ts">
  import Toast from "./Toast.svelte";

  import { dismissToast, toasts } from "../stores/toasts";
</script>

{#if $toasts}
  <section>
    {#each $toasts as toast (toast.id)}
      <Toast
        type={toast.type}
        dismissible={toast.dismissable}
        on:dismiss={() =>
          toast.id !== null && toast.id !== undefined
            ? dismissToast(toast.id)
            : ""}
        ><p class="text-white text-sm font-normal">{toast.message}</p></Toast
      >
    {/each}
  </section>
{/if}

<style lang="postcss">
  section {
    position: fixed;
    top: 0;
    right: 0;
    width: 100%;
    display: flex;
    margin-top: 1rem;
    align-items: start;
    justify-content: center;
    flex-direction: column;
    z-index: 1000;
  }
</style>
