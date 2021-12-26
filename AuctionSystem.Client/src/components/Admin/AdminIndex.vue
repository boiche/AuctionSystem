<template>
    <div class="container-fluid accordion" role="tablist">
        <b-card no-body class="mb-1">
            <b-card-header header-tag="header" class="p-1" role="tab">
                <b-button block v-b-toggle.collapse-users accordion="adminSections" role="tablist" class="m-1 w-100">Users</b-button>
            </b-card-header>
            <b-collapse id="collapse-users" visible accordion="adminSections" role="tablist">
                <b-card-body>
                    <AdminUsers></AdminUsers>
                </b-card-body>
            </b-collapse>
        </b-card>

        <b-card no-body class="mb-1">
            <b-card-header header-tag="header" class="p-1" role="tab">
                <b-button block v-b-toggle.collapse-auctions accordion="adminSections" role="tablist" class="m-1 w-100">Auctions</b-button>
            </b-card-header>
            <b-collapse id="collapse-auctions" accordion="adminSections" role="tablist">
                <AdminAuctions></AdminAuctions>
            </b-collapse>
        </b-card>
    </div>    
</template>

<script>
    import AdminUsers from '@/components/Admin/AdminUsers.vue'
    import AdminAuctions from '@/components/Admin/AdminAuctions.vue'

    export default {
        components: {
            AdminUsers,
            AdminAuctions
        }
    }
</script>

<style>
    .round-panel {
        border-radius: 25px;
        border: 2px solid black;
        padding: 20px;
        width: 200px;
        height: 150px;
        margin: 0px 5px;
    }

    #table {
        margin: 0 auto;
        width: 500px;
    }

    #table .input-group-text {
        padding: 0 0.5em 0 0.5em;
    }

    #table .input-group-text .fa {
        font-size: 12px;
    }
</style>