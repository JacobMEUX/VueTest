<template>
  <div>
    <div @click="showModal">
      <slot/>
    </div>
    <a-modal v-model="visible" :closable="false" :maskClosable="false">
      <a-card
        style="width:100%"
        :tabList="tabList"
        :activeTabKey="selected"
        @tabChange="key => onTabChange(key, 'selected')"
      >
        <div>
          <Clothing v-show="selected === 'clothing'" ref="clothingForm"/>
          <Brand v-show="selected === 'brand'" ref="brandForm"/>
          <Category v-show="selected === 'category'" ref="categoryForm"/>
        </div>
      </a-card>
      <template slot="footer">
        <a-button key="back" @click="onClose">Cancel</a-button>
        <a-button key="submit" type="primary" :loading="creating" @click.prevent="onCreate">Create</a-button>
      </template>
    </a-modal>
  </div>
</template>

<script>
import Clothing from "./itemTabs/Clothing";
import Category from "./itemTabs/Category";
import Brand from "./itemTabs/Brand";

import itemTabsMixin from "../../mixins/itemTabs.js";

export default {
  components: {
    Clothing,
    Category,
    Brand
  },
  mixins: [itemTabsMixin],
  data() {
    return {
      creating: false,
      visible: false,
    };
  },
  methods: {
    showModal() {
      this.visible = true;
    },
    onClose() {
      this.visible = false;
    },
    onCreate() {
      this.creating = true;
      const dto = {
        title: this.$refs.clothingForm.title,
        description: this.$refs.clothingForm.description,
        fkCategoryId: this.$refs.categoryForm.categoryId,
        fkBrandId: this.$refs.brandForm.brandId,
        price: parseFloat(this.$refs.clothingForm.price),
        image: {
          altText: this.$refs.clothingForm.title,
          url: this.$refs.clothingForm.image
        }
      };

      this.$api
        .insertClothes(dto)
        .then(() => {
          this.$message.success("The item has been created");
          this.visible = false;
        })
        .catch(err => {
          this.$message.error(err);
        })
        .finally(() => {
          this.creating = false;
        });
    }
  }
};
</script>