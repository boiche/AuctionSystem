<template>
    <div class="container-fluid">
        <div id="wrapper">
            <form class="formInput">
                <h3>Create Auction</h3>
                <label for="title">Title</label>
                <input v-model="title" name="title" type="text" id="title" />
                <label for="description">Description</label>
                <input v-model="description" id="description" type="text" name="description" />
                <label for="endDate">End date</label>
                <input v-model="endDate" id="description" type="datetime-local" name="description" />
                <!--<label for="picture">Picture</label>
                <input name="picture" type="file" accept="image/" id="picture" />-->
                <div class="btn btn-submit" name="submit" type="submit" id="submit" v-on:click="submit()">Create</div>
            </form>
        </div>
    </div>
</template>

<script>
    import AuctionService from '@/services/auctionService.js'

    export default {
        data() {
            return {
                title: '',
                description: '',
                file: '',
                endDate: ''
            }
        },
        methods: {
            async submit () {
                await AuctionService.createAuction(this.title, this.description, Date.now, this.endDate);
                this.$router.push('/')
            }
        }
    }
</script>

<style scoped>
    #formInput {
        padding: 15px 25px;
        display: flex;
        gap: 10px;
        justify-content: center;
    }
    div.btn {
        font-family: "Lato";
    }
    #wrapper {
        margin: 0 auto;
        padding-bottom: 25px;
        background: #eee;
        width: 600px;
        max-width: 100%;
        border: 2px solid #212121;
        border-radius: 4px;
    }
    #title, #picture, #description {
        display: block;
        margin: 10px;
        width: 80%;
        margin-left:10%;
        flex: 1;
        border-radius: 4px;
        border: 1px solid #ff9800;
        padding: 7px;
    }
    #submit {
        background: #ff9800;
        border: 2px solid #e65100;
        color: white;
        font-weight: bold;
        border-radius: 4px;
        cursor: pointer !important;
        width: 30%;
        margin-top: 10px;
    }
</style>