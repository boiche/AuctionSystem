<template>
    <div id="auctionsTable">
        <b-table striped hover :items="items" :fields="fields">
            <template #cell(Actions)="row">
                <div class="wider">
                    <b-button size="sm" @click="bidss(row.item)" class="m-1" v-if="row.item.stateId == 0">Preview bids</b-button>
                    <b-button variant="primary" size="sm" @click="edit(row.item)" v-if="row.item.stateId == 0" class="m-1">Edit</b-button>
                    <b-button variant="danger" size="sm" @click="suspend(row.item)" v-if="row.item.stateId == 0" class="m-1">Suspend</b-button>
                    <div v-if="row.item.stateId > 0">N/A</div>
                </div>
            </template>
        </b-table>
        <b-modal :id="suspendModal.id" :title="suspendModal.title" hide-footer no-close-on-backdrop>
            <b-form @submit="submitSuspension" class="m-1">
                <div class="d-block text-center">
                    Are you sure?
                </div>
                <div class="d-flex justify-content-center">
                    <b-button class="mt-3 m-5 w-25" type="submit" block variant="success">Yes</b-button>
                    <b-button class="mt-3 m-5 w-25" block variant="danger" @click="closeModal(suspendModal.id)">No</b-button>
                </div>
            </b-form>
        </b-modal>
        <b-modal :id="editModal.id" :title="editModal.title" hide-footer no-close-on-backdrop>
            <b-form @submit="submitEdit" class="m-1">
                <b-form-group>
                    <b-form-input v-model="editForm.id" hidden></b-form-input>
                    <b-form-input v-model="editForm.title" type="text" class="m-1" required placeholder="Title"></b-form-input>
                    <b-form-input v-model="editForm.description" type="text" class="m-1" required placeholder="Description"></b-form-input>
                </b-form-group>
                <div class="d-block text-center">
                    Save changes?
                </div>
                <div class="d-flex justify-content-center">
                    <b-button class="mt-3 m-5 w-25" type="submit" block variant="success">Yes</b-button>
                    <b-button class="mt-3 m-5 w-25" block variant="danger" @click="closeModal(editModal.id)">No</b-button>
                </div>
            </b-form>
        </b-modal>
        <b-modal :id="bidsModal.id" :title="bidsModal.title" size="xl" hide-footer no-close-on-backdrop>
            <AdminBids :auctionId="auctionId"></AdminBids>
        </b-modal>
    </div>
</template>

<script>
    import AuctionService from '@/services/auctionService.js'
    import AdminBids from '@/components/Admin/AdminBids.vue'

    export default {
        components: {
            AdminBids
        },
        data() {
            return {
                dataArray: null,
                fields: [
                    { key: 'title', label: 'Title', sortable: true },
                    { key: 'publishedOn', label: 'Published On', sortable: true },
                    { key: 'description', label: 'Description', sortable: true },
                    { key: 'state.name', label: 'State', sortable: true },
                    { key: 'Actions' }
                ],
                editModal: {
                    id: 'edit-modal',
                    title: 'Edit Auction',
                    content: ''                   
                },
                suspendModal: {
                    id: 'suspend-modal',
                    title: 'Suspend auction',
                    content: ''
                },
                bidsModal: {
                    id: 'bids-modal',
                    title: '',
                    content: ''
                },
                editForm: {
                    id: '',
                    title: '',
                    description: ''
                },
                auctionId: ''
            }
        },
        async mounted() {
            this.dataArray = await AuctionService.getAuctions();
        },
        computed: {
            items() {
                if (this.dataArray == undefined) return;
                return this.dataArray;
            }
        },
        methods: {
            bidss(item) {
                this.$bvModal.show(this.bidsModal.id)
                this.auctionId = item.id
                this.bidsModal.title = `Bids for ${item.title}`
            },
            edit(item) {
                this.$bvModal.show(this.editModal.id)
                this.editModal.title = `Edit Auction ${item.title}`
                this.editForm.title = item.title
                this.editForm.description = item.description
                this.editForm.id = item.id
            },
            suspend(item) {
                this.$bvModal.show(this.suspendModal.id)
                this.suspendModal.title = `You are about to suspend auction ${item.title}`
                this.auctionId = item.id
            },
            closeModal(modalId) {
                this.$bvModal.hide(modalId)
            },
            async submitSuspension() {
                await AuctionService.suspendAuction(this.auctionId)
            },
            submitEdit() {
                AuctionService.editAuction(this.editForm)
            }
        }
    }
</script>