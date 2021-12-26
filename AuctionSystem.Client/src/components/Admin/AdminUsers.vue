<template>
    <div>
        <b-table striped hover :items="items" :fields="fields">
            <template #cell(Actions)="row">
                <div class="wider">
                    <b-button variant="danger" size="sm" @click="info(row.item, true)" v-if="!row.item.banDate" class="m-1">Ban</b-button>
                    <b-button variant="success" size="sm" @click="info(row.item, false)" v-if="row.item.banDate" class="m-1">Unban</b-button>
                </div>
            </template>
        </b-table>
        <b-modal :id="infoModal.id" :title="infoModal.title" hide-footer no-close-on-backdrop>
            <b-form @submit="submit" class="m-1">
                <b-form-group v-if="infoModal.reason">
                    <b-form-input v-model="form.userId" hidden></b-form-input>
                    <b-form-input v-model="form.banDate" :min="today" type="date" class="m-1" required></b-form-input>
                    <b-form-input v-model="form.banReason" typeof="text" class="m-1" required placeholder="Reason"></b-form-input>
                </b-form-group>
                <div class="d-block text-center">
                    Are you sure?
                </div>
                <div class="d-flex justify-content-center">
                    <b-button class="mt-3 m-5 w-25" type="submit" block variant="success">Yes</b-button>
                    <b-button class="mt-3 m-5 w-25" block variant="danger" @click="closeModal">No</b-button>
                </div>
            </b-form>            
        </b-modal>
    </div>
</template>

<script>
    import AuthService from '@/services/authService.js'
    import moment from 'moment'
    export default {
        data() {
            return {
                keyword: '',
                today: moment(new Date()).format('YYYY-MM-DD'),
                dataArray: null,
                fields: [
                    { key: 'firstName', label: 'First name', sortable: true },
                    { key: 'thirdName', label: 'Last name', sortable: true },
                    { key: 'email', label: 'Email', sortable: true },
                    { key: 'phone', label: 'Phone', sortable: true },
                    { key: 'Actions' }
                ],
                infoModal: {
                    id: 'info-modal',
                    title: '',
                    reason: false
                },
                form: {
                    userId: '',
                    banDate: '',
                    banReason: ''
                }
            }
        },
        async mounted() {
            this.dataArray = await AuthService.getUsers();
        },
        computed: {
            items() {
                if (this.dataArray == undefined) return;
                return this.dataArray;
            }
        },
        methods: {
            info(item, reason) {
                this.$bvModal.show(this.infoModal.id)
                if (reason == true) {
                    this.infoModal.title = `You are about to ban user ${item.username}`
                    this.infoModal.reason = true
                    this.form.userId = item.id
                }
                else {
                    this.infoModal.title = `You are about to unban user ${item.username}`
                    this.infoModal.reason = false
                }
            },
            closeModal() {
                this.$bvModal.hide(this.infoModal.id)
            },
            submit() {
                AuthService.banUser(this.form)
            }
        }
    }
</script>

<style scoped>
    .wider {
        width:200px;
    }
</style>